document.addEventListener("alpine:init", () => {
    Alpine.data("category", () => ({
        _list: [],
        _modal: {},
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
            this._modal = new bootstrap.Modal("#categoryUpdinModal");
            //chuyển về chữ thường của ô input
            this.$watch("_updinData.name", (newVal, oldVal) => {
                this._updinData.slug = newVal.toLowerCase()
                    .normalize("NFD")
                    .replace(/[\u0300-\u036f]/g, "")
                    .replace("đ", "d")
                    .replace(/[^a-z]/g, "-");
            });

            fetch("/Admin/Category/ListAll")
                .then(x => x.json())
                .then(json => {
                    this._list = json;
                })
                .catch(err => {
                    console.log(err);
                })
        },
        openModalAdd() {
            this._modal.show();
            this._modalSetting = {
                title: "Thêm danh mục sản phẩm",
                url: "", //để sau
                primaryButtonText: "Thêm mới",
            }
        },
        openModalUpdate() {
            this._modal.show();
            this._modalSetting = {
                title: "Cập nhật danh mục sản phẩm",
                url: "", //để sau
                primaryButtonText: "Cập nhật",
            }
        },
        saveCategory() {

        }
    }));
});