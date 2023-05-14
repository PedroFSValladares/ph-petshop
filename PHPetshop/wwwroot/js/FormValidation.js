export default function validateForm() {
    let forms = document.querySelectorAll(".require-validation")
    forms.forEach((form) => {
        let formButton = form.querySelector("button")
        let formInputs = form.querySelectorAll("input")
        let divFeedback = document.querySelector(".email-feedback")

        formInputs.forEach((input) => {
            input.addEventListener("input", (event) => {
                divFeedback.innerText = "Campo obrigatório."
                if (input.required) {
                    validate(input)
                }
            })
        })

        formButton.addEventListener("click", (event) => {
            validate(Array.from(formInputs))
            validateEmail(formInputs[0])
        })

        form.addEventListener("submit", (event) => {
            if (!validateEmail(formInputs[0])) {
                event.preventDefault()
            }
        })
        /*
        */
    })
}
function validate(input) {
    if (Array.isArray(input)) {
        input.forEach((e) => {
            testValue(e)
        })
    } else {
        testValue(input)
    }
}

function testValue(input) {
    if (input.value == "") {
        input.classList.add("is-invalid")
    } else {
        input.classList.remove("is-invalid")
    }
}

function validateEmail(input) {
    let emailRegex = /\S+@\S+\.\S+/
    let divFeedback = document.querySelector(".email-feedback")
    if (emailRegex.test(input.value)) {
        input.classList.remove("is-invalid")
        return true
    } else {
        input.classList.add("is-invalid")
        divFeedback.innerText = "Insira um Email válido."
        return false
    }
}
