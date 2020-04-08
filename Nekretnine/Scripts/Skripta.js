$(document).ready(function () {

    // PODACI OD INTERESA *****************************************************************************************
    var host = window.location.host;
    var nekretnineEndpoint = "/api/nekretnine/";
    var agentiEndpoint = "/api/agenti/";
    var token = null;
    var headers = {};
    var editedId;


    // PUNJENJE TABELE PODACIMA ************************************************************************************
    getNekretnine();


    // KLIK ********************************************************************************************************
    $("body").on("click", "#registracijaPrijava", divPrijava);
    $("body").on("click", "#btnPocetak", divPocetak);
    $("body").on("click", "#btnEditItem", editItem);
    $("body").on("click", "#btnDeleteItem", deleteItem);
    $("body").on("click", "#btnQuit", quit);


    // KLIK NA DUGME REGISTRACIJA I PRIJAVA *************************************************************************************
    function divPrijava() {
        document.getElementById("infoPrijava").classList.add("hidden");
        document.getElementById("divPrijava").classList.remove("hidden");
    }


    // KLIK NA DUGME POCETAK ******************************************************************************************
    function divPocetak() {
        document.getElementById("infoPrijava").classList.remove("hidden");
        document.getElementById("divPrijava").classList.add("hidden");
        document.getElementById("prijavljen").innerHTML = "";
        clearForm();
    }


    // REGISTRACIJA KORISNIKA ***************************************************************************************
    $("#btnRegistracija").click(function () {

        var email = $("#priEmail").val();
        var loz1 = $("#priLoz").val();

        // objekat koji se salje
        var sendData = {
            "Email": email,
            "Password": loz1,
            "ConfirmPassword": loz1
        };

        $.ajax({
            type: "POST",
            url: 'http://' + host + "/api/Account/Register",
            data: sendData

        }).done(function (data) {
            console.log(data);
            alert("Uspešna registracija na sistem!");
            clearForm();

        }).fail(function (data) {
            console.log(data);
            alert("Greška prilikom registracije.");
        });


    });


    // PRIJAVA KORISNIKA ********************************************************************************************
    $("#formPrijava").submit(function (e) {
        e.preventDefault();

        var email = $("#priEmail").val();
        var loz = $("#priLoz").val();

        // objekat koji se salje
        var sendData = {
            "grant_type": "password",
            "username": email,
            "password": loz
        };

        $.ajax({
            "type": "POST",
            "url": 'http://' + host + "/Token",
            "data": sendData

        }).done(function (data) {
            console.log(data);
            $("#info").empty().append("Prijavljen korisnik: " + data.userName);
            token = data.access_token;
            tokenType = data.token_type;

            document.getElementById("divPrijava").classList.add("hidden");
            document.getElementById("infoOdjava").classList.remove("hidden");
            $("#prijavljen").empty().append(data.userName);
            document.getElementById("pretraga").classList.remove("hidden");
            document.getElementById("ulogovan").classList.add("tabelalog");
            clearForm();

            let elements = document.getElementsByClassName("showHide");
            for (var i = 0; i < elements.length; i++) {
                elements[i].classList.remove("hidden");
            }

            popuniPadajuciMeni();
            getNekretnine();

        }).fail(function (data) {
            alert("Greška prilikom prijave.");
        });

    });

    // ODJAVA ******************************************************************************************************
    $("#odjava").click(function () {
        token = null;
        headers = {};
        formAction = "Create";
        editingId = "";

        $("#prijavljen").empty();
        document.getElementById("infoOdjava").classList.add("hidden");
        document.getElementById("infoPrijava").classList.remove("hidden");
        document.getElementById("pretraga").classList.add("hidden");
        document.getElementById("izmena").classList.add("hidden");
        document.getElementById("ulogovan").classList.remove("tabelalog");

        let elements = document.getElementsByClassName("showHide");
        for (var i = 0; i < elements.length; i++) {
            elements[i].classList.add("hidden");
        }

        getNekretnine();
    });


    // PUNJENJE TABELE PODACIMA *******************************************************
    function popuniTabelu(data) {
        $("#tabelaPodaci").empty();

        var body = document.getElementById('tabela').getElementsByTagName('tbody')[0];
        for (var i = 0; i < data.length; i++) {
            var row = body.insertRow(i);

            var cell0 = row.insertCell(0);
            var cell1 = row.insertCell(1);
            var cell2 = row.insertCell(2);
            var cell3 = row.insertCell(3);
            var cell4 = row.insertCell(4);
            var cell5 = row.insertCell(5);
            var cell6 = row.insertCell(6);
            var cell7 = row.insertCell(7);

            cell0.innerHTML = data[i].Mesto;
            cell1.innerHTML = data[i].AgencijskaOznaka;
            cell2.innerHTML = data[i].GodinaIzgradnje;
            cell3.innerHTML = data[i].Kvadratura;
            cell4.innerHTML = data[i].Cena;
            cell5.innerHTML = data[i].Agent.ImePrezime;

            cell6.classList.add("showHide");
            cell7.classList.add("showHide");
            if (!token) {
                cell6.classList.add("hidden");
                cell7.classList.add("hidden");
            }

            var btn1 = document.createElement('button');
            var btn2 = document.createElement('button');
            btn1.classList.add("btn", "btn-default");
            btn2.classList.add("btn", "btn-default");
            btn1.textContent = "Obrisi";
            btn2.textContent = "Izmeni";
            btn1.value = data[i].Id;
            btn2.value = data[i].Id;
            btn1.name = "id";
            btn2.name = "id";
            btn1.id = "btnDeleteItem";
            btn2.id = "btnEditItem";
            cell6.appendChild(btn1);
            cell7.appendChild(btn2);
        }
    }


    // AJAX GET ITEMS *************************************************************
    function getNekretnine() {
        $.ajax({
            type: "GET",
            url: 'http://' + host + nekretnineEndpoint
        })
            .done(function (data) {
                popuniTabelu(data);
            })
            .fail(function (data) {
                alert("Doslo je do greške prilikom dobavljanja podataka");
            });
    }



    // PUNJENJE PADAJUCEG MENIJA ***********************************************************************************
    function popuniPadajuciMeni() {
        var select = document.getElementById("selektovan");

        $.ajax({
            url: 'http://' + host + agentiEndpoint,
            type: "GET"
        })
            .done(function (data) {
                if ($('#selektovan option').length === 0) {
                    for (var i = 0; i < data.length; i++) {
                        if (select) {
                            select.options[i] = new Option(data[i].ImePrezime, data[i].Id);
                        }
                    }
                }
            })
            .fail(function (data) {
                alert("Desila se greška prilikom popunjavanja padajuceg menija.");
            });
    }

    // CLICK SUBMIT PRETRAGA *******************************************************
    $("#formPretraga").submit(function (e) {
        e.preventDefault();

        if (token) {
            headers.Authorization = 'Bearer ' + token;

            var mini = document.getElementById("najmanje").value;
            var maksi = document.getElementById("najvise").value;

            // objekat koji se salje
            let sendData = {
                "Mini": mini,
                "Maksi": maksi
            };

            $.ajax({
                type: "POST",
                url: 'http://' + host + "/api/pretraga",
                data: sendData,
                "headers": headers
            })
                .done(function (data) {
                    popuniTabelu(data);
                    document.getElementById("najmanje").value = "";
                    document.getElementById("najvise").value = "";
                })
                .fail(function (data) {
                    alert("Greska prilikom pretrage!");

                });

        }
        else {
            alert("Morate biti ulogovani.");
        }
    });


    // BRISANJE STAVKE *********************************************************************************************
    function deleteItem() {
        var deleteId = this.value;

        if (token) {
            headers.Authorization = 'Bearer ' + token;
            $.ajax({
                url: 'http://' + host + nekretnineEndpoint + deleteId,
                type: "DELETE",
                "headers": headers
            })
                .done(function (data) {
                    getNekretnine();
                })
                .fail(function (data) {
                    alert("Desila se greška prilikom brisanja stavke.");
                });
        }
        else {
            alert("Morate biti ulogovani da biste obrisali stavku.");
        }
    }


    // KLIK na dugme odustajanje **********************************************************************************
    function quit() {
        document.getElementById("itemMesto").value = "";
        document.getElementById("itemAgencijskaOznaka").value = "";
        document.getElementById("itemGodinaIzgradnje").value = "";
        document.getElementById("selektovan").value = 1;
        document.getElementById("itemKvadratura").value = "";
        document.getElementById("itemCena").value = "";
        document.getElementById("izmena").classList.add("hidden");

    }


    // dobavljanje STAVKE ************************************************************
    function editItem() {
        var editId = this.value;

        if (token) {
            headers.Authorization = 'Bearer ' + token;

            // saljemo zahtev da dobavimo stavku
            $.ajax({
                url: 'http://' + host + nekretnineEndpoint + editId,
                type: "GET",
                "headers": headers
            })
                .done(function (data) {
                    document.getElementById("izmena").classList.remove("hidden");
                    document.getElementById("itemMesto").value = data.Mesto;
                    document.getElementById("itemAgencijskaOznaka").value = data.AgencijskaOznaka;
                    document.getElementById("itemGodinaIzgradnje").value = data.GodinaIzgradnje;
                    document.getElementById("selektovan").value = data.AgentId;
                    document.getElementById("itemKvadratura").value = data.Kvadratura;
                    document.getElementById("itemCena").value = data.Cena;

                    editedId = data.Id;

                    formAction = "Update";
                })
                .fail(function (data) {
                    document.getElementById("izmena").classList.add("hidden");
                    formAction = "Create";
                    alert("Greška prilikom izmene.");
                });
        } else {
            alert("Morate biti ulogovani.");
        }
    }

    // IZMENA STAVKE *********************************************************************************************************************
    $("#addEditForm").submit(function (e) {
        // sprecavanje default akcije forme
        e.preventDefault();

        var Mesto = $("#itemMesto").val();
        var GodinaIzgradnje = $("#itemGodinaIzgradnje").val();
        var Agent = $("#selektovan").val();
        var AgencijskaOznaka = $("#itemAgencijskaOznaka").val();
        var Kvadratura = $("#itemKvadratura").val();
        var Cena = $("#itemCena").val();
        var httpAction = "PUT";

        var url = "http://" + host + nekretnineEndpoint + editedId;
        var sendData = {
            "Id": editedId,
            "Mesto": Mesto,
            "AgencijskaOznaka": AgencijskaOznaka,
            "GodinaIzgradnje": GodinaIzgradnje,
            "Kvadratura": Kvadratura,
            "Cena": Cena,
            "AgentId": Agent
        };

        console.log("Objekat za slanje");
        console.log(sendData);

        $.ajax({
            url: url,
            type: httpAction,
            "headers": headers,
            data: sendData
        })
            .done(function (data) {
                formAction = "Create";
                document.getElementById("izmena").classList.add("hidden");
                getNekretnine();

            })
            .fail(function (data) {
                alert("Greška prilikom izmene!");
            });

    });

    function clearForm() {
        document.getElementById("priEmail").value = "";
        document.getElementById("priLoz").value = "";

    }

});