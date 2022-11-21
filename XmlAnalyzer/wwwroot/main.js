function onMultiSelectRendered() {
    const inputs = [...document.querySelectorAll('.selectInput')];
    const optionContainers = [...document.querySelectorAll('.options')];
    const containers = [...document.querySelectorAll('.container')];
    const chooseAllInputs = [...document.querySelectorAll('.chooseAll input')];
    const clearAllButtons = [...document.querySelectorAll('.clearFilters input')];
    const customSelect= document.querySelector('.customSelectField');
    const main = document.body;

    let currentInput = {
        element: null,
        index: null,
    };

    chooseAllInputs.forEach(input => {
        input.addEventListener('change', () => {
            input.parentElement.parentElement
                .querySelectorAll(':scope label:not(.chooseAll):not(.hidden) input[type=checkbox]')
                .forEach(i => i.checked = input.checked);
        });
    });

    clearAllButtons.forEach(button => {
        button.addEventListener('click', () => {
            button.parentElement.parentElement
                .querySelectorAll(':scope input[type=checkbox]')
                .forEach(i => i.checked = false);
        });
    });

    inputs.forEach((input, index) => {
        input.addEventListener('focus', () => {
            if (currentInput.element !== input) {
                optionContainers[currentInput.index]?.classList.remove('visible');
            }

            optionContainers[index].classList.add('visible');
            currentInput.element = input;
            currentInput.index = index;
        });
    });

    customSelect.addEventListener('click', (e) => {
        if (customSelect.classList.contains('active'))
            return;
        customSelect.classList.add('active');
        customSelect.nextElementSibling.classList.add('visible');
        optionContainers[currentInput.index]?.classList.remove('visible');
        e.stopPropagation();
    });

    main.addEventListener('click', (e) => {
        customSelect.classList.remove('active');
        customSelect.nextElementSibling.classList.remove('visible');

        if (currentInput.element === null)
            return;

        if ((e.target.parentElement !== containers[currentInput?.index]
            && e.target.parentElement?.parentElement !== containers[currentInput?.index]
            && e.target.parentElement?.parentElement?.parentElement !== containers[currentInput?.index]))
        {
            let container = optionContainers[currentInput.index];
            container?.classList.remove('visible');
            let checkboxes = [...container?.querySelectorAll(':scope input[type=checkbox]')];
            let marked = 0;
            checkboxes.forEach(c => { if (c.checked) marked++; });
            currentInput.element.value = marked != 0 ? `${Math.min(marked, checkboxes.length - 1)} вибрано` : '';
        } else {
            currentInput?.element.focus();
        }
    });
}

function markCheckbox(name, value) {
    document.querySelector(`.chooseAll input[name=${name}]`).checked = value;
}

function setInputValue(name, value) {
    document.querySelector(`input[type=text][name=${name}]`).value = value;
}

function clearFilters() {
    let filtersContainers = [...document.querySelectorAll('.multiSelect .container')];
    filtersContainers.forEach(container => {
        container.firstChild.value = '';
        container.querySelectorAll(':scope input[type=checkbox]')
            .forEach(box => box.checked = false);
    });
}

async function downloadFile(fileName, contentStreamReference) {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
}