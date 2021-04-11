
$("#addEducationContentForm").submit(function (event) {
    event.preventDefault();

    debugger;

    var formValue = (this);

    swal({
        text: "Are you sure you want to save the training content",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Evet',
        cancelButtonText: 'Hayır',
        confirmButtonClass: 'btn btn-success',
        cancelButtonClass: 'btn btn-danger m-l-5',
        buttonsStyling: false
    }).then(function (willDelete) {
        if (willDelete) {
            $.ajax({
                method: "POST",
                url: "/Education/AddEducationContent",
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                data: new FormData(formValue),
                cache: false,
                contentType: false,
                processData: false,
                beforeSend: function () {

                }
            }).done(function (d) {
                if (d.failed == true) {
                    toastr.error(d.message, "Hata");

                } else {
                    $('#partialEducationContent').html(d);
                }


            }).always(function () {

            });
        }

    });



});
