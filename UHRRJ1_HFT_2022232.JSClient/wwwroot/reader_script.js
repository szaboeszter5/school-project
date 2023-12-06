let Readers = [];
let connection = null;
let i = 1;
getdata();
setupSignalR();

let ReaderIdToUpdate = -1;


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:23125/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ReaderCreated", (user, message) => {
        getdata();
    });

    connection.on("ReaderDeleted", (user, message) => {
        getdata();
    });

    connection.on("ReaderUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:23125/Reader')
        .then(x => x.json())
        .then(y => {
            Readers = y;
            console.log(y);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    Readers.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr>" +
            "<td>" + t.readerId + "</td>" +
            "<td>" + t.readerName + "</td>" +
            "<td>" + `<button type="button" onclick="remove(${t.readerId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.readerId})">Update</button>` +
        `<button type="button" onclick="books('${encodeURIComponent(JSON.stringify(t.readerName))}')">Books</button>` +
        "</td>" + "<td>" +
            `<button type="button" onclick="authors_books('${encodeURIComponent(JSON.stringify(t.readerName))}')">Show</button>` +
            "</td>" +
            "</tr>";
    });
}

function showupdate(id) {
    document.getElementById('readernametoupdate').value = Readers.find(t => t['readerId'] == id)['readername'];
    document.getElementById('updateformdiv').style.display = 'block';
    ReaderIdToUpdate = id;
}

function remove(id) {
    fetch('http://localhost:23125/Reader/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('readernametoupdate').value;
    fetch('http://localhost:23125/Reader', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { readerName: name, readerId: ReaderIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let name = document.getElementById('readername').value;
    fetch('http://localhost:23125/Reader', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { readerName: name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function books(arg) {
    document.getElementById('noncrud_area').innerHTML = "";
    name = JSON.parse(decodeURIComponent(arg));
    url = 'http://localhost:23125/OwnedBooks/OwnedBooks?' + new URLSearchParams({ readerName: name }).toString();
    fetch(url)
        .then(response => response.json())
        .then(data => {
            console.log('Success:', data);
            data.forEach(book => {
                document.getElementById('noncrud_area').innerHTML += "<tr><td>" + book.title + "</td></tr>";
                i++;
            })
        })
        .catch((error) => { console.error('Error:', error); });

    console.log("Fetched from: ");
    console.log(url);
}

function authors_books(arg) {
    document.getElementById('noncrud_area').innerHTML = "<thead><tr><td>Name</td><td>Books</td></tr></thead>";
    name = JSON.parse(decodeURIComponent(arg));
    url = 'http://localhost:23125/ReadersAuthorsAndBooks/FavouriteAuthor?' + new URLSearchParams({ readerName: name }).toString();
    fetch(url)
        .then(response => response.json())
        .then(data => {
            console.log('Success:', data);
            data.forEach(t => {
                document.getElementById('noncrud_area').innerHTML += "<tr><td>" + t.name + "</td><td>" + t.bookCount + "</td></tr>";
                i++;
            })
        })
        .catch((error) => { console.error('Error:', error); });

    console.log("Fetched from: ");
    console.log(url);
}