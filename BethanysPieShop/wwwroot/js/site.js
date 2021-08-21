$(function () {
    if ($("a.confirmDelete").length) {
        ($("a.confirmDelete").click(() => {
            if (!confirm("confirm delete")) return false;
        }));
    }

    if ($("div.alert.notification").length) {
        setTimeout(() => {
            $("div.alert.notification").fadeOut();
        }, 200);
    }
});

function readURL(input) {
    if (input.files && input.files[0]) {
        let reader = new FileReader();

        reader.onload = function (e) {
            $("img#ImageUpload").attr("src", e.target.result).width(200).height(200);
        };

        reader.readAsDataURL(input.files[0]);
    }
}