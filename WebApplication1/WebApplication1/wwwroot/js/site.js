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
    PlaceHolderElement.on('click', '[]')


    $('#example1').dataTable({
        "dom": "fltip",
        "searching": true,
        "responsive": true,
        "lengthChange": false,
        "ordering": true,
        "paging": true,
        "autoWidth": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]


    }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');


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

                $.ajax({
                    type: 'GET',
                    url: "https://localhost:44363/Item/GetItem",
                    success: function (res) {
                        $(document).Toasts('create', {
                            class: 'bg-success',
                            title: 'Success !',
                            subtitle: 'Item Created',
                            body: 'You created a new Item !'
                        })

                    }
                })
               
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

                $.ajax({
                    type: 'GET',
                    url: "https://localhost:44363/Item/GetItem",
                    success: function (res) {
                        $(document).Toasts('create', {
                            class: 'bg-success',
                            title: 'Success !',
                            subtitle: 'Item Updated',
                            body: 'You updated a new Item !'
                        })

                    }
                })

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