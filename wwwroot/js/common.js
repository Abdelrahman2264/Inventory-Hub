function ShowConfirmationModal() {
    bootstrap.Modal.getOrCreateInstance(document.getElementById('bsConfirmationModal')).show();
}

function HideConfirmationModal() {
    bootstrap.Modal.getOrCreateInstance(document.getElementById('bsConfirmationModal')).hide();
}
function ShowFormModal() {
    bootstrap.Modal.getOrCreateInstance(document.getElementById('FormModal')).show();
}

function HideFormModal() {
    bootstrap.Modal.getOrCreateInstance(document.getElementById('FormModal')).hide();
}
function ShowDeskLapFormModal() {
    bootstrap.Modal.getOrCreateInstance(document.getElementById('DeskLapFormModal')).show();
}

function HideDeskLapFormModal() {
    bootstrap.Modal.getOrCreateInstance(document.getElementById('DeskLapFormModal')).hide();
} function ShowMaintainReturnFormModal() {
    bootstrap.Modal.getOrCreateInstance(document.getElementById('MaintainReturn')).show();
}

function HideMaintainReturnFormModal() {
    bootstrap.Modal.getOrCreateInstance(document.getElementById('MaintainReturn')).hide();
}
function downloadFile(fileName, base64Data) {
    const link = document.createElement('a');
    link.href = 'data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,' + base64Data;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
function openPrintPreview(pdfDataUrl) {
    var win = window.open(pdfDataUrl, '_blank');
    if (win) {
        win.onload = function () {
            win.print();
        };
    }
}
function startTypingEffect(text, elementId = "animatedText") {
    const targetElement = document.getElementById(elementId);
    if (!targetElement) return;

    let index = 0;
    const lines = text.split('\n');
    targetElement.innerHTML = "";

    function typeLine() {
        if (index < lines.length) {
            const line = lines[index];
            let charIndex = 0;

            function typeChar() {
                if (charIndex < line.length) {
                    targetElement.innerHTML += line.charAt(charIndex);
                    charIndex++;
                    setTimeout(typeChar, 100); // Typing speed (100ms per character)
                } else {
                    targetElement.innerHTML += "<br>"; // Move to the next line
                    index++;
                    setTimeout(typeLine, 500); // Delay before starting the next line
                }
            }

            typeChar();
        }
    }

    typeLine();
}