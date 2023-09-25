document.addEventListener("alpine:init", () => {
    Alpine.data("category", () => ({
        _list: [],
        _modal: {},
        _noti: {},
        //thay đổi cái phần url: tên và link
        _modalSetting: {
            title: "",
            url: "",
            primaryButtonText: "",
        },

        _updinData: {
            id: 0,
            name: "",
            slug: "",
        },

        init() {
            var config = {
                durations: {
                    success: 2000
                },
                labels: {
                    success: "Thành công"
                }
            };
            this._modal = new bootstrap.Modal("#categoryUpdinModal");
            this._noti = new AWN(config); //AWN là awesome-notifications
            //chuyển về chữ thường của ô input
            this.$watch("_updinData.name", (newVal, oldVal) => {
                this._updinData.slug = newVal.toLowerCase()
                    .normalize("NFD")
                    .replace(/[\u0300-\u036f]/g, "")
                    .replace(/đ/g, "d")
                    .replace(/[^a-z]/g, "-");
            });

            this.refreshData();
        },
        refreshData() {
            fetch("/Admin/Category/ListAll")
                .then(x => x.json())
                .then(json => {
                    this._list = json;
                })
                .catch(err => {
                    console.log(err);
                });
        },
        openModalAdd() {
            this._modal.show();
            this._modalSetting = {
                title: "Thêm danh mục sản phẩm",
                url: "/Admin/Category/UpSert",
                primaryButtonText: "Thêm mới",
            }
            //xóa dữ liệu khi mở modal add
            this._updinData = {
                id: 0,
                name: "",
                slug: "",
            };
        },
        //truyền id
        openModalUpdate(id) {
            this._modal.show();
            this._modalSetting = {
                title: "Cập nhật danh mục sản phẩm",
                url: "/Admin/Category/UpSert/" + id,
                primaryButtonText: "Cập nhật",
            }
            //lấy dữ liệu khi thao tác update
            fetch("/Admin/Category/Detail/" + id)
                .then(res => res.json())
                .then(json => {
                    this._updinData = json
                });
        },
        saveCategory() {
            fetch(this._modalSetting.url, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(this._updinData) // stringify chuyển thành chuổi dạng json(text)
            })
                .then((res) => {
                    this._modal.hide();
                    return res.text();
                })
                .then(text => {
                    this._noti.success(text);
                    this.refreshData();
                })
                .catch(err => {
                    alert("Lỗi !!!");
                });
        },
        deleteCategory(id) {
            var url = "/Admin/Category/Delete/" + id;
            this._noti.confirm("Chắc chưa", async () => {
                await fetch(url);
                this._noti.success("Xóa thành công !");
                this.refreshData();
            });
        },
    }));
});