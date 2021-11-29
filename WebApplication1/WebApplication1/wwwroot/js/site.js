// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    var PlaceHolderElement = $('#PlaceHolderHere');
    $('#CreateItem').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })

    $('.DeleteItem').click(function (event) {
        var url = $(this).data('url');
        url = decodeURIComponent(url);
        var a = 5;
        $.get(url).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })

    $('.EditItem').click(function (event) {
        var url = $(this).data('url');
        url = decodeURIComponent(url);
        $.get(url).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })
    PlaceHolderElement.on('click','[]')
})



var showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
            // to make popup draggable
            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });
        }
    })
}

var jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                console.log(res.success);
                $('#modal-default .modal-body').html('');
                $('#modal-default .modal-title').html('');
                $('#modal-default').modal('hide');
                location.reload();
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

var jQueryAjaxPut = form => {
    url2 = decodeURIComponent(form.action)
    try {
        $.ajax({
            type: 'PUT',
            url: url2,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                console.log(res.success);
                $('#modal-default .modal-body').html('');
                $('#modal-default .modal-title').html('');
                $('#modal-default').modal('hide');
                location.reload();
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

var jQueryAjaxDelete = form => {
    try {
        $.ajax({
            type: 'POST',
            url: 'DeleteConfirmed' ,
            data: null,
            processData: false,
            contentType: false,
            success: function (res) {
                $('#deleteConfirm').modal('hide');
                $('#view-all').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
    } catch (ex) {
        console.log(ex)
}
    

    //prevent default form submit event
    return false;
}