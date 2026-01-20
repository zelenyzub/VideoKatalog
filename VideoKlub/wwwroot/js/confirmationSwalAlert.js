$(document).ready(function () {

    // Generic confirmation for deleting items via hidden form
    function confirmAction(options) {
        $(options.selector).on('click', function (e) {
            e.preventDefault();

            var itemId = $(this).data(options.idDataAttr);

            Swal.fire({
                title: options.confirmTitle || 'Da li ste sigurni?',
                text: options.confirmText || 'Ova akcija je nepovratna!',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: options.confirmButtonText || 'Da, nastavi!',
                cancelButtonText: options.cancelButtonText || 'Otkaži'
            }).then((result) => {
                if (result.isConfirmed) {
                    // submituje skriveni form
                    $('#deleteForm-' + itemId).submit();
                }
            });
        });
    }

    // Poziv za delete video
    confirmAction({
        selector: '.delete-video-btn',
        idDataAttr: 'id',
        confirmTitle: 'Da li ste sigurni?',
        confirmText: 'Ova akcija će trajno obrisati video!',
        confirmButtonText: 'Da, obriši!',
        cancelButtonText: 'Otkaži'
    });
});
