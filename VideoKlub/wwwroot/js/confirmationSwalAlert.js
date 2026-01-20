$(document).ready(function () {

    // Generic confirmation for deleting items via hidden form
    function confirmAction(options) {
        $(options.selector).on('click', function (e) {
            e.preventDefault();

            var itemId = $(this).data(options.idDataAttr);
            var formPrefix = options.formPrefix || 'deleteForm-'; // default za video

            Swal.fire({
                title: options.confirmTitle || 'Da li ste sigurni?',
                html: options.confirmHtml
                    ? options.confirmHtml
                    : (options.confirmText || 'Ova akcija je nepovratna!'),
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: options.confirmButtonText || 'Da, nastavi!',
                cancelButtonText: options.cancelButtonText || 'Otkaži'
            }).then((result) => {
                if (result.isConfirmed) {
                    // submituje skriveni form sa odgovarajućim prefix-om
                    $('#' + formPrefix + itemId).submit();
                }
            });
        });
    }

    // Delete video
    confirmAction({
        selector: '.delete-video-btn',
        idDataAttr: 'id',
        formPrefix: 'deleteForm-', // video forme imaju prefix deleteForm-
        confirmTitle: 'Da li ste sigurni?',
        confirmText: 'Ova akcija će trajno obrisati video!',
        confirmButtonText: 'Da, obriši!',
        cancelButtonText: 'Otkaži'
    });

    // Delete category
    confirmAction({
        selector: '.delete-category-btn',
        idDataAttr: 'id',
        formPrefix: 'deleteCategoryForm-',
        confirmTitle: 'Pažnja!',
        confirmHtml: `
        <div class="alert alert-dismissible bg-light-danger border border-danger d-flex flex-column flex-sm-row p-5 mb-10">
            <div class="d-flex flex-column pe-0 pe-sm-10">
            <h5 class="mb-1">Ova akcija je nepovratna!</h5>
                <span>Ova akcija će trajno obrisati kategoriju i celokupan video sadržaj vezan za tu kategoriju!</span>
            </div>
        </div>
    `,
        confirmButtonText: 'Da, obriši!',
        cancelButtonText: 'Otkaži'
    });


});
