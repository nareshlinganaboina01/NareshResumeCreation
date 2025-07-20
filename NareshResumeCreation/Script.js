// ==================== Validation Functions ====================
// Function to validate email format
function validateEmail(email) {
    const emailPattern = /^[^@\s]+@[^@\s]+\.[^@\s]+$/;
    return emailPattern.test(email);
}

// Function to validate phone number format
function validatePhone(phone) {
    const phonePattern = /^\d{10}$/; // Assumes a 10-digit phone number
    return phonePattern.test(phone);
}

// Function to validate URL format
function validateURL(url) {
    const urlPattern = /^(https?:\/\/[^\s]+)$/;
    return urlPattern.test(url);
}

// ==================== Character Counter ====================
// Function to update character count for textareas
function updateCharCount(textarea, charCountId, maxLength) {
    const charCount = document.getElementById(charCountId);
    const remaining = maxLength - textarea.value.length;
    charCount.textContent = `${remaining} characters remaining`;
    if (remaining < 0) {
        charCount.style.color = "red";
    } else {
        charCount.style.color = "black";
    }
}

// ==================== Image Upload and Preview ====================
// Function to preview uploaded image
function previewImage(event) {
    const fileInput = event.target;
    const imgPreview = document.getElementById("imgPreview");
    if (fileInput.files && fileInput.files[0]) {
        const reader = new FileReader();
        reader.onload = function (e) {
            imgPreview.src = e.target.result;
        };
        reader.readAsDataURL(fileInput.files[0]);
    }
}

// Function to handle drag-and-drop file upload
function loadImage(event) {
    const fileInput = event.target;
    const imgPreview = document.getElementById("imgPreview");
    if (fileInput.files && fileInput.files[0]) {
        const reader = new FileReader();
        reader.onload = function (e) {
            imgPreview.src = e.target.result;
        };
        reader.readAsDataURL(fileInput.files[0]);
    }
}

// ==================== Form Validation and Submission ====================
// Function to validate the entire form before submission
function validateForm() {
    let isValid = true;

    // Validate Job Title
    const jobTitle = document.getElementById("txtJobTitle");
    if (!jobTitle.value.trim()) {
        isValid = false;
        jobTitle.classList.add("error");
    } else {
        jobTitle.classList.remove("error");
    }

    // Validate First Name
    const firstName = document.getElementById("txtFirstName");
    if (!firstName.value.trim()) {
        isValid = false;
        firstName.classList.add("error");
    } else {
        firstName.classList.remove("error");
    }

    // Validate Last Name
    const lastName = document.getElementById("txtLastName");
    if (!lastName.value.trim()) {
        isValid = false;
        lastName.classList.add("error");
    } else {
        lastName.classList.remove("error");
    }

    // Validate Email
    const email = document.getElementById("txtEmail");
    if (!email.value.trim() || !validateEmail(email.value)) {
        isValid = false;
        email.classList.add("error");
    } else {
        email.classList.remove("error");
    }

    // Validate Phone
    const phone = document.getElementById("txtPhone");
    if (!phone.value.trim() || !validatePhone(phone.value)) {
        isValid = false;
        phone.classList.add("error");
    } else {
        phone.classList.remove("error");
    }

    return isValid;
}




// Function to handle form submission
function handleFormSubmit(event) {
    if (!validateForm()) {
        event.preventDefault(); // Prevent form submission if validation fails
        alert("Please fill out all required fields correctly.");
    } else {
        saveFormData(); // Save form data before submission
    }
}

// ==================== Event Listeners ====================
document.addEventListener("DOMContentLoaded", function () {
    // Load saved form data
    loadFormData();

    // Add event listener for form submission
    const form = document.getElementById("form1");
    form.addEventListener("submit", handleFormSubmit);

    // Add event listener for image upload
    const fileUpload = document.getElementById("filePhoto");
    if (fileUpload) {
        fileUpload.addEventListener("change", previewImage);
    }

    // Add event listener for drag-and-drop file upload
    const dragFileInput = document.getElementById("dragFileInput");
    if (dragFileInput) {
        dragFileInput.addEventListener("change", loadImage);
    }

    // Add event listener for character count in summary
    const summaryTextarea = document.getElementById("txtSummary");
    if (summaryTextarea) {
        summaryTextarea.addEventListener("input", function () {
            updateCharCount(this, "charCountSummary", 300);
        });
    }

    // Add event listener for dynamic form toggling
    document.querySelectorAll(".btn-add").forEach(button => {
        button.addEventListener("click", function () {
            const formId = this.getAttribute("data-target");
            toggleFormVisibility(formId);
        });
    });

    // Add event listener for closing forms
    document.querySelectorAll(".btn-close-form").forEach(button => {
        button.addEventListener("click", function () {
            const formId = this.getAttribute("data-target");
            toggleFormVisibility(formId);
        });
    });

    // Add event listener for deleting entries
    document.querySelectorAll(".btn-delete").forEach(button => {
        button.addEventListener("click", function () {
            const entryId = this.getAttribute("data-id");
            deleteEntry(entryId);
        });
    });
});

// ==================== Form Toggling ====================
// Function to toggle visibility of forms
function toggleFormVisibility(formId) {
    const form = document.getElementById(formId);
    if (form) {
        form.style.display = form.style.display === "none" ? "block" : "none";
    }
}

// ==================== Delete Entry ====================
// Function to delete an entry (e.g., Employment, Education, etc.)
function deleteEntry(entryId) {
    if (confirm("Are you sure you want to delete this entry?")) {
        // Perform deletion logic here (e.g., AJAX request or DOM manipulation)
        console.log(`Deleting entry with ID: ${entryId}`);
    }
}

// ==================== Additional Event Listeners ====================
// Example: Toggle Employment Form
document.getElementById("btnAddEmployment").addEventListener("click", function () {
    toggleFormVisibility("employmentForm");
});

// Example: Toggle Education Form
document.getElementById("btnAddEducation").addEventListener("click", function () {
    toggleFormVisibility("educationForm");
});

// Example: Toggle Skills Form
document.getElementById("btnAddSkill").addEventListener("click", function () {
    toggleFormVisibility("skillForm");
});

// Example: Toggle Internship Form
document.getElementById("btnAddInternship").addEventListener("click", function () {
    toggleFormVisibility("internshipForm");
});

// Example: Toggle Reference Form
document.getElementById("btnAddReference").addEventListener("click", function () {
    toggleFormVisibility("referenceForm");
});


// ==================== Drag and Drop ====================
// Handle drag-and-drop for file upload
let dropZone = $('#dropZone');
let fileInput = $('#dragFileInput');

dropZone.on('dragover', function (e) {
    e.preventDefault();
    dropZone.css('background-color', '#e0e0ff');
});

dropZone.on('dragleave', function () {
    dropZone.css('background-color', '#f8f9ff');
});

dropZone.on('drop', function (e) {
    e.preventDefault();
    dropZone.css('background-color', '#f8f9ff');
    let files = e.originalEvent.dataTransfer.files;
    if (files.length > 0) {
        fileInput[0].files = files;
        $('#dragFileInput').trigger('change');
    }
});

dropZone.on('click', function () {
    fileInput.click();
});
