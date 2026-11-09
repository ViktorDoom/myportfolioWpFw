// Dit script is gegenereerd met AI voor deliverable 2.

const contactForm = document.getElementById("contact-form");
const formStatus = document.getElementById("form-status");
const submitButton = contactForm.querySelector('button[type="submit"]');

const fields = [
    { input: contactForm.elements.namedItem("name"), label: "your name" },
    { input: contactForm.elements.namedItem("email"), label: "your email address" },
    { input: contactForm.elements.namedItem("subject"), label: "a subject" },
    { input: contactForm.elements.namedItem("message"), label: "a message" }
];

let isSubmitting = false;

function validateField({ input, label }) {
    const value = input.value.trim();
    const errorElement = document.getElementById(`${input.id}-error`);
    let error = "";

    if (input.required && value.length === 0) {
        error = `Please enter ${label}.`;
    } else if (input.validity.typeMismatch) {
        error = "Enter a valid email address, such as name@example.com.";
    } else if (input.minLength >= 0 && value.length < input.minLength) {
        error = `Enter at least ${input.minLength} characters.`;
    } else if (input.maxLength >= 0 && value.length > input.maxLength) {
        error = `Enter no more than ${input.maxLength} characters.`;
    }

    if (errorElement.textContent !== error) {
        errorElement.textContent = error;
    }

    if (error) {
        input.setAttribute("aria-invalid", "true");
    } else {
        input.removeAttribute("aria-invalid");
    }

    return error === "";
}

for (const field of fields) {
    field.input.addEventListener("input", () => {
        if (field.input.hasAttribute("aria-invalid")) {
            validateField(field);
        }
    });
}

contactForm.addEventListener("submit", async (event) => {
    event.preventDefault();

    if (isSubmitting) {
        return;
    }

    // Evaluate every field so all inline errors appear in one attempt.
    const invalidFields = fields.filter(field => !validateField(field));

    if (invalidFields.length > 0) {
        formStatus.textContent = "Correct the fields with errors and submit again.";
        invalidFields[0].input.focus();
        return;
    }

    const contactRequest = Object.fromEntries(
        fields.map(({ input }) => [input.name, input.value.trim()])
    );

    isSubmitting = true;
    submitButton.disabled = true;
    formStatus.textContent = "All fields are valid. Sending your message…";

    try {
        const response = await fetch("/api/contact", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(contactRequest)
        });

        if (!response.ok) {
            formStatus.textContent = response.status === 400
                ? "The server rejected your input. Check your details and try again."
                : "The server could not accept your message. Please try again.";
            return;
        }

        // Preserve any edits the user made while the request was in flight.
        const formMatchesRequest = fields.every(
            ({ input }) => input.value.trim() === contactRequest[input.name]
        );

        if (formMatchesRequest) {
            contactForm.reset();
        }

        formStatus.textContent = "Your message was sent successfully.";
    } catch (error) {
        console.error("Contact form submission failed:", error);
        formStatus.textContent =
            "Could not confirm that your message was sent. Check your connection and try again.";
    } finally {
        isSubmitting = false;
        submitButton.disabled = false;
    }
});

// Enable custom validation once the submit handler is registered.
contactForm.noValidate = true;