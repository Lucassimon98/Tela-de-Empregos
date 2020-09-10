function showPopupSuccess(message, functionClose)
{
    noty(
        {
    text: message,
            type: 'success',
            timeout: 3000,
            modal: true,
            callback:
        {
        onClose: function() {
                functionClose();
            }
        }
    });

}

function showPopupError(message)
{
    noty(
        {
    text: message,
            type: 'error',
            timeout: 3000,
            modal: true
        });
}