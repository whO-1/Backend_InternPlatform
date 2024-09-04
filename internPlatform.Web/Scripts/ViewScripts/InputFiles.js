document.addEventListener('DOMContentLoaded', function () {
    const fileInput = document.getElementById('formFileMultiple');
    const selectedFilesContainer = document.getElementById('selectedFiles');
    const removeButton = document.getElementById('removeButton');
    const maxFileSize = 2 * 1024 * 1024; 
    const maxFilesAllowed = 5; 
    const imageIdsInput = document.getElementById('imageIds');

    fileInput.addEventListener('change', handleFileSelect);

    function handleFileSelect(event) {
        const files = event.target.files;
        
        const existingImagesCount = document.querySelectorAll('.image-element').length;
        const totalFilesCount = existingImagesCount + files.length;

        if (totalFilesCount > maxFilesAllowed) {
            displayError(`You can only upload up to ${maxFilesAllowed} images. Please remove some files.`);
            fileInput.value = ''; 
            return;
        }

        displaySelectedFiles(files);
    }

    function displaySelectedFiles(files) {
        for (let i = 0; i < files.length; i++) {
            const file = files[i];
            // Check if the file is an image
            if (!file.type.startsWith('image/')) {
                displayError(`File ${file.name} is not an image.`);
                continue;
            }
            // Check if the file size exceeds the maximum limit
            if (file.size > maxFileSize) {
                displayError(`File ${file.name} exceeds the 2 MB size limit.`);
                continue;
            }

            // Create a spinner while the file is uploading
            const spinner = document.createElement('div');
            spinner.classList.add('loader');
            selectedFilesContainer.appendChild(spinner);

            // Upload the file and handle the response
            uploadFile(file, function (response) {
                const img = document.createElement('img');
                img.src = URL.createObjectURL(file);
                img.classList.add('selected-image', 'image-element'); // Add the image-element class to the uploaded image
                selectedFilesContainer.replaceChild(img, spinner);

                // Update the image IDs in the hidden input field
                const currentIds = imageIdsInput.value ? imageIdsInput.value.split(',') : [];
                currentIds.push(response.data);
                imageIdsInput.value = currentIds.join(',');
            });
        }
    }

    function uploadFile(file, callback) {
        const xhr = new XMLHttpRequest();
        const formData = new FormData();
        formData.append('file', file);

        xhr.open('POST', '/Admin/Dashboard/ImagesBuffer', true);

        xhr.onload = function () {
            if (xhr.status === 200) {
                const response = JSON.parse(xhr.responseText);
                callback(response);
            } else {
                console.error('Error uploading file');
                displayError('Error uploading file');
            }
        };

        xhr.onerror = function () {
            console.error('Error uploading file');
            displayError('Error uploading file');
        };

        xhr.send(formData);
    }

    function displayError(message) {
        const errorDiv = document.createElement('div');
        errorDiv.classList.add('error-message');
        errorDiv.textContent = message;
        selectedFilesContainer.appendChild(errorDiv);
    }

    removeButton.addEventListener('click', function () {
        fileInput.value = ''; // Clear the file input
        selectedFilesContainer.innerHTML = ''; // Clear the image previews and errors
        imageIdsInput.value = ''; // Clear the hidden input field
    });
});
