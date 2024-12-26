function OpenAddImageForm() {
    // create input element
    const fileInput = document.createElement("input");
    fileInput.type = "file";
    fileInput.id = "productImageInput";
    fileInput.accept = "image/jpeg, image/jpg";
    fileInput.hidden = true;

    fileInput.addEventListener("change", () => {
        const file = fileInput.files[0];
        if (file) {
            uploadImage(file);
        }
    });

    fileInput.click(); // open it
}

function ReplaceImageForm(id) {
    // create input element
    const fileInput = document.createElement("input");
    fileInput.type = "file";
    fileInput.id = "productImageInput";
    fileInput.accept = "image/jpeg, image/jpg";
    fileInput.hidden = true;

    fileInput.addEventListener("change", () => {
        const file = fileInput.files[0];
        if (file) {
            replaceImage(file, id);
        }
    });

    fileInput.click(); // open it
}

async function uploadImage(file) {
    try {
        // create form with data
        const formData = new FormData();
        formData.append("image", file);

        // Make request to server
        const response = await fetch("/Home/AddImage", {
            method: 'POST',
            body: formData
        });

        if (response.ok) {
            // console.log("Image uploaded successfully!");
            // location.reload();
        } else { }
    } catch (error) {
        console.error("Error uploading image:", error);
    }

    location.reload();
}

async function replaceImage(file, id) {
    try {
        // create form with data
        const formData = new FormData();
        formData.append("image", file);
        formData.append("id", id);

        // Make request to server
        const response = await fetch("/Home/EditImage", {
            method: 'POST',
            body: formData
        });

        if (response.ok) {
            // console.log("Image uploaded successfully!");
            // location.reload();
        } else { }
    } catch (error) {
        console.error("Error uploading image:", error);
    }

    location.reload();
}