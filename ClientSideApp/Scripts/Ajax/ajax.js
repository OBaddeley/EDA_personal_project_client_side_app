$(document).ready(function() {

    //list all customers
    $('#listCustomerButton').click(function getCustomers() {

        $.ajax({
            type: "GET",
            url: "http://localhost:62379/api/Customers",
            success: function(data) {
                for (var i = 0; i < data.length; i++) {
                    var customerData = data[i];
                    renderCustomers(customerData);
                }
            },
            error: function() {
                console.log("Error in ajax api GET");
            }

        });
    });


    function renderCustomers(customerData) {
        var htmlString = "<li class='list'>" +
            "<div class='customerID' id='" + customerData.Id + "'>" + customerData.Id + "</div>" + " | " + customerData.Name + " | " + customerData.Email + " | " + customerData.Phone_number +
            " " + "<a class= 'More'>More</a>" + " | " + "<a class= 'Delete'>Delete</a>" + " " + "</li>";
        $(".customerlist").append(htmlString);
    }


//delete a customer

    //need to get id

    $(document).on('click', '.Delete', function deleteCustomer(e) {

        var thing = $(this);
        var thisID = $(this).siblings('.customerID').html();

        $.ajax({
            type: "DELETE",
            url: "http://localhost:62379/api/Customers/" + thisID,

            success: function() {
                var parent = $(thing).parents('li');

                parent.remove();
            },
            error: function() {
                console.log("didnt delete customer");
            }
        });
    });

    function renderNote(data) {
        var htmlString = data;
        $('.customer-note').append(htmlString);
    }

    function renderNoteForm() {
        var htmlString = "<h2>"+"Notes"+"</h2>"+"<form id='noteForm'>" +
            "<input type='text' name='Text' class ='formText' value='add a note'>" +
            "<input type='submit'>" +
            "</form>";
        $('.customer-note').append(htmlString);
    }

    //look at notes for a customer
    // GET: api/Customers/5/notes
    //need id

    $(document).on('click', '.More', function seeCustomerNote() {

        var thisID = $(this).siblings('.customerID').html();
        //console.log(thisID);
        $.ajax({
            type: "GET",
            url: "api/Customers/" + thisID + "/notes",

            success: function(data) {
                //console.log(data);   
                renderNoteForm();
                renderNote(data);
                $(".notes").attr('id', thisID);

            },
            error: function() {
                console.log("didnt get customer note");
            }

        });

    });

  


    //add note for customer
    //POST: api/Customers/5/notes

    $('.customer-note').on('submit', '#noteForm', function(e) {
        e.preventDefault();

        var id = $(e.target).next().attr('id');
        var thissubmit = $(e.target).children('.formText').val();

 
        $.ajax({
            type: "POST",
            url: "/api/Customers/" + id + "/notes",
            data: {'': thissubmit},

            success: function(res) {
                console.log("worked");
                $('#noteForm')[0].reset();  //clears form
                
                //show new note without refreshing page      
            },
            error: function() {
                console.log("no data");
            }
        });
    });

//notes for trying to get the POST a note to work:
    //console.log(thissubmit); //the text put in the form
    //console.log(this);  //the form
    //console.log(e.target); //the form
    //console.log("etarget" +$(e.target));
    //console.log($(e.target).serialize());

    // console.log("I have been clicked submit");

    //var data = $(e.target).serialize();   // need to parse data in as JSON object e.g Text={hello}

    //var data = { "" : };
    //data.Note.Content = thissubmit.serialize();

    //console.log(data);
    //var data = $(e.target);
    //var json = JSON.parse(data);
    //var jsonobj = $(data); //Syntax error, unrecognized expression:
    //console.log(jsonobj);


    /**********************************************************************/
    //add a customer
    //need to make a form
    //    ///// function () {

    //    $.ajax({
    //        type: "POST",
    //        url: "http://localhost:62379/api/Customers",
    //    data: { name: ""name, Email: "email", Phone_number : "phone" },
    //        success: function (newData) {

    //                //where going to put the data
    //                // get data as needed
    //                //?renderCustomers(newData);
    //            },
    //        error: function () {
    //           console.log("didnt add customer");
    //        }

    //    });
    //}
});