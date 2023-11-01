﻿namespace EShopMVCNet7.Common
{
    public enum UserRole
    {
        //Trạng thái gán 1 và 2; ko gán thì thằng nào nằm trên là 0 ...-> n
        ROLECUSTOMER = 1,
        ROLEADMIN = 2,
    }
    public enum OrderStatus
    {
        /*Đánh số tự động  0-> n*/
        PENDING,
        APPROVED,
        DELIVERED,
        CANCELED 
    }
}
