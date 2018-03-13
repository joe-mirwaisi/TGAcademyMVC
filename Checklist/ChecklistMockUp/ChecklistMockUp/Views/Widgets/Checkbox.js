$(function () {
    $("input[type=checkbox]").click(function () {
        var data_id = $(this).data("id");
        $.ajax({
            url: 'Home/Edit',
            type: 'POST',
            data: { id: data_id, isChecked: $(this).is(':checked') },
            success: function (result) {
            }
        });
    });
});