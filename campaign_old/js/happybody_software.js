function registaLog(id, codigo, notas) {
    $.ajax({
        type: "POST",
        url: "login.aspx/RegistaLog",
        data: '{"id_operador":"' + id + '", "tipo_log":"' + codigo + '", "notas":"' + notas + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (res) {
            if (res.d != null) {
                var result = res.d;

                if (result != null) {
                    if (parseInt(result) > 0) {

                    }
                    else {

                    }
                }
            }
        }
    });
}