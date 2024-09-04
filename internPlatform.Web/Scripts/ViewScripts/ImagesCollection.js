document.addEventListener('DOMContentLoaded', function () {
    const imgBlocks = document.querySelectorAll('.imgBlock');
    const bigImage = document.querySelector('.entityImg img');
    const imageIds = document.getElementById("StoredImagesIds");
    const mainImageIdInput = document.getElementById("MainImageId"); // Target the input element

    // Add event listener to each remove button
    function DeleteButtons() {
        const removeButtons = document.querySelectorAll('.removeImg');
        removeButtons.forEach(function (button) {
            button.addEventListener('click', function (e) {
                try {
                    let parent = button.closest('.imgBlock');
                    parent.style.display = "none";

                    if (e.target.nextSibling.nextSibling.id.startsWith('image-')) {
                        const idToRemove = e.target.nextSibling.nextSibling.id.substring(6);
                        let arr = imageIds.value.split(',');
                        const index = arr.indexOf(idToRemove);
                        if (index >= 0) {
                            arr.splice(index, 1);
                            imageIds.value = arr.join(',');
                        }
                    }
                } catch (err) {
                    console.error(err.message);
                }
            });
        });
    }
    DeleteButtons();

    // Initial setup of event listeners on image blocks
    setupEventListeners();

    // Drag and drop variables
    let dragSrcEl = null;

    // Event listener setup function
    function setupEventListeners() {
        imgBlocks.forEach(function (imgBlock) {
            const img = imgBlock.querySelector('img');
            img.addEventListener('click', function () {
                bigImage.src = img.src;
                // Update the MainImageId input value to the image's ID (removing the 'image-' prefix)
                if (img.id.startsWith('image-')) {
                    mainImageIdInput.value = img.id.slice(6);
                }
            });

            imgBlock.addEventListener('dragstart', handleDragStart);
            imgBlock.addEventListener('dragover', handleDragOver);
            imgBlock.addEventListener('drop', handleDrop);
            imgBlock.addEventListener('dragend', handleDragEnd);
        });
    }

    function handleDragStart(e) {
        dragSrcEl = this;
        e.dataTransfer.effectAllowed = 'move';
        e.dataTransfer.setData('text/html', this.innerHTML);
        this.classList.add('dragging');
    }

    function handleDragOver(e) {
        if (e.preventDefault) {
            e.preventDefault();
        }
        e.dataTransfer.dropEffect = 'move';
        return false;
    }

    function handleDrop(e) {
        if (e.stopPropagation) {
            e.stopPropagation();
        }
        if (dragSrcEl !== this) {
            dragSrcEl.innerHTML = this.innerHTML;
            this.innerHTML = e.dataTransfer.getData('text/html');

            DeleteButtons();
            setupEventListeners(); // Re-setup event listeners after a drop
            updateImageOrder();
        }
        return false;
    }

    function handleDragEnd() {
        this.classList.remove('dragging');
        imgBlocks.forEach(function (imgBlock) {
            imgBlock.classList.remove('over');
        });
    }

    function updateImageOrder() {
        const elements = document.querySelectorAll('.image-element');
        const ids = [];
        elements.forEach(element => {
            if (element.closest('.imgBlock').style.display !== "none" && element.id.startsWith('image-')) {
                ids.push(element.id.substring(6));
            }
        });
        imageIds.value = ids.join(",");
    }
});
