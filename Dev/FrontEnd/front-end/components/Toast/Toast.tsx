import toaststyle from './Toast.module.css'


function Toast({message}: {message: string}){
    return (
        <div className={toaststyle.toast}>
            <p>{message}</p>
        </div>
    )
}

export default Toast;