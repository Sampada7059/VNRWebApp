$(document).ready(function () {
    
    $('#loginButton').click(function () {
        var username = $('#Username').val();
        var password = $('#Password').val();

        // Validate if username and password are not empty
        if (!username && !password)
        {
            $(".PerrorMessage").text("Please Enter Username and Password"); 
            return;
        } else {
            $(".PerrorMessage").text(""); // Clear the error message if password is not empty
        }
        if (!username) {
            $(".UerrorMessage").text("Please Enter Username"); 
            return;
        } else {
            $(".UerrorMessage").text(""); // Clear the error message if username is not empty
        }

        if (!password) {
            $(".PerrorMessage").text("Please Enter Password"); 
            return;
        } else {
            $(".PerrorMessage").text(""); // Clear the error message if password is not empty
        }
        // Show the loader while the API call is in progress
        $("#loader").show();
        
        //Ajax call for API
        $.ajax({
            type: 'POST',
            url: 'https://localhost:7110/api/Users/Login',
            data: JSON.stringify({
                Username: username,
                Password: password
            }),
            contentType: 'application/json',
            success: function (result)
            {
                // Hide the loader after the API call is complete
                $("#loader").hide();
                if (result.nameOfUser)
                {
                    $("#loginForm").submit();//Valid credential->Submit the form
                }
                else
                {
                    $(".PerrorMessage").text("Invalid Username or Password");//Invalid credential
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $("#loader").hide();
                alert('Server Side Error');//server side error
            }
        });
       
    // Add event handlers for input fields to clear the error messages when the user starts typing
    });
    $('#Username').on('input', function () {
        $(".UerrorMessage").text("");
    });

    $('#Password').on('input', function () {
        $(".PerrorMessage").text("");
    });
});