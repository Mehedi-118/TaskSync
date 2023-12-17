window.addEventListener("load", (event) => {
    console.log("Page is fully loaded");

    autoTokenGenerationEvent();
});

function autoTokenGenerationEvent() {
    setTimeout(() => {
        var authorizeModalOpenButtonRef = document.querySelector("#swagger-ui > section > div.swagger-ui > div:nth-child(2) > div.scheme-container > section > div > button");

        authorizeModalOpenButtonRef.addEventListener("click", (event) => {
            setTimeout(() => {
                //var accessTokenRemoveButtonRef = document.querySelector("#swagger-ui > section > div.swagger-ui > div:nth-child(2) > div.scheme-container > section > div > div > div.modal-ux > div > div > div.modal-ux-content > div > form > div.auth-btn-wrapper > button:nth-child(1)");

                //if (!accessTokenRemoveButtonRef || (accessTokenRemoveButtonRef && accessTokenRemoveButtonRef.innerHTML !== 'Authorize')) {
                //    generateMessageFieldOnly();
                //    setNotificationMessage("To get new Access Token click on Logout button.");
                //    accessTokenRemoveButtonRef.addEventListener("click", (event) => {
                //        generateLoginInputFields();
                //    });
                //} else {
                //    generateLoginInputFields();
                //}

                generateLoginInputFields();
            }, 100);
        });

        const unlockLinks = document.querySelectorAll('button.authorization__btn.unlocked');
        for (var element of unlockLinks) {
            element.addEventListener("click", (event) => {
                generateLoginInputFields();
                //setAccessToken();
            });
        }

        const lockLinks = document.querySelectorAll('button.authorization__btn.locked');
        for (var element of lockLinks) {
            element.addEventListener("click", (event) => {
                generateLoginInputFields();
                //setAccessToken();
            });
        }
    }, 5000);
}

function generateLoginInputFields() {
    if (document.querySelector("#username")) {
        return false;
    }

    setTimeout(() => {
        var form = document.querySelector("#swagger-ui > section > div.swagger-ui > div:nth-child(2) > div.scheme-container > section > div > div > div.modal-ux > div > div > div.modal-ux-content > div > form");

        // Create a wrapper element
        const wrapper = document.createElement('div');
        wrapper.innerHTML = `
                                <div class="auth-btn-wrapper" class="loginDiv">
                                    <div class="wrapper">
                                        <label>Username:</label>
                                        <section class=""><input type="text" aria-label="auth-bearer-value" id="username"></section>
                                    </div>
                                    <div class="wrapper">
                                        <label>Password:</label>
                                        <section class=""><input type="password" aria-label="auth-bearer-value" id="password"></section>
                                    </div>
                            </div>

                            <div class="auth-btn-wrapper loginDiv"><button type="submit"
                                    class="btn modal-btn auth authorize button" id="login">Login</button><button
                                    class="btn modal-btn auth btn-done button" id="clear">Clear</button>
                            </div>

                            <small class="auth-btn-wrapper"><b id="message"></b></small>
    `;

        // Insert the wrapper element at the beginning of the form
        form.insertBefore(wrapper, form.firstChild);

        var loginBtn = document.querySelector("#login");
        var clearBtn = document.querySelector("#clear");

        loginBtn.addEventListener("click", (event) => {
            var username = document.querySelector("#username").value;
            var password = document.querySelector("#password").value;

            if (username && password) {
                setAccessToken(username, password);
            }
        });

        clearBtn.addEventListener("click", (event) => {
            clearUsernamePasswordMessage()
        });
    }, 1);
}

function generateMessageFieldOnly() {
    var form = document.querySelector("#swagger-ui > section > div.swagger-ui > div:nth-child(2) > div.scheme-container > section > div > div > div.modal-ux > div > div > div.modal-ux-content > div > form");

    // Create a wrapper element
    const wrapper = document.createElement('div');
    wrapper.innerHTML = `
                            <div class="loginDiv">
                                <small class="auth-btn-wrapper"><b id="message"></b></small>
                            </div>
    `;

    // Insert the wrapper element at the beginning of the form
    form.insertBefore(wrapper, form.firstChild);
}

function clearUsernamePasswordMessage() {
    document.querySelector("#username").value = "";
    document.querySelector("#password").value = "";
    document.querySelector("#message").innerHTML = "";
}

function copyAccessTokenToClipBoard(username, password) {
    var accessTokenSetButtonRef = document.querySelector("#swagger-ui > section > div.swagger-ui > div:nth-child(2) > div.scheme-container > section > div > div > div.modal-ux > div > div > div.modal-ux-content > div > form > div.auth-btn-wrapper > button.btn.modal-btn.auth.authorize.button");
    accessTokenSetButtonRef.removeAttribute("type");

    var errorMsg = "Invalid Credentials!";

    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            if (this.responseText) {
                copyToClipBoard(this.responseText);
                var accessTokenSetInputRef = document.querySelector("#swagger-ui > section > div.swagger-ui > div:nth-child(2) > div.scheme-container > section > div > div > div.modal-ux > div > div > div.modal-ux-content > div > form > div:nth-child(2) > div:nth-child(3) > section > input[type=text]")
                //accessTokenSetInputRef.value = this.responseText;
                accessTokenSetInputRef.focus();
            } else {
                setNotificationMessage(errorMsg, true);
            }
        } else if (this.readyState == 4) {
            setNotificationMessage(errorMsg, true);
        }
    };
    xhttp.open("POST", `../api/Account/AccessToken?userName=${username}&userPassword=${password}&RememberMe=true`, true);
    xhttp.send();
}

function copyToClipBoard(value) {
    // text area method
    let textArea = document.createElement("textarea");
    textArea.value = value;
    // make the textarea out of viewport
    textArea.style.position = "fixed";
    textArea.style.left = "-999999px";
    textArea.style.top = "-999999px";
    document.body.appendChild(textArea);
    textArea.focus();
    textArea.select();
    return new Promise((res, rej) => {
        // here the magic happens
        document.execCommand('copy') ? res() : rej();
        textArea.remove();
        setNotificationMessage("Access Token is copied to clipboard. Pres Ctrl+V and click the authorize button.");
    });
}

function setNotificationMessage(text, isError = false) {
    document.querySelector("#message").innerHTML = text;
    document.querySelector("#message").style.color = isError ? "red" : "#49cc90";
}

function setAccessToken(username, password) {
    setTimeout(() => {
        var accessTokenSetButtonRef = document.querySelector("#swagger-ui > section > div.swagger-ui > div:nth-child(2) > div.scheme-container > section > div > div > div.modal-ux > div > div > div.modal-ux-content > div:nth-child(1) > form > div.auth-btn-wrapper > button.btn.modal-btn.auth.authorize.button");
        var accessTokenRemoveButtonRef = document.querySelector("#swagger-ui > section > div.swagger-ui > div:nth-child(2) > div.scheme-container > section > div > div > div.modal-ux > div > div > div.modal-ux-content > div > form > div.auth-btn-wrapper > button:nth-child(1)");

        if (accessTokenSetButtonRef) {
            copyAccessTokenToClipBoard(username, password);
        } else if (accessTokenRemoveButtonRef) {
            accessTokenRemoveButtonRef.click();
            copyAccessTokenToClipBoard(username, password);
        }
    }, 100);
}