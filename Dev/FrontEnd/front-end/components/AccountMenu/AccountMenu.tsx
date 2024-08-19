import AccountMenuStyles from "./AccountMenuStyles.module.css";
function AccountMenu()
{
    return(
        <div className={AccountMenuStyles.accountMenu}>
            <p>Manage Account</p>
            <p>Orders</p>
            <p>Cart</p>
        </div>
    )
};

export default AccountMenu;