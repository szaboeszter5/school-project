let readers = [];
let connection = null;
let i = 1;
getdata();
setupSignalR();

let readerIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:23125/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("readerCreated", (user, message) => {
        getdata();
    });

    connection.on("readerDeleted", (user, message) => {
        getdata();
    });

    connection.on("readerUpdated", (user, message) => {
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
    await fetch('http://localhost:23125/reader')
        .then(x => x.json())
        .then(y => {
            readers = y;
            console.log(y);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    readers.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr>" +
            "<td>" + t.readerId + "</td>" +
            "<td>" + t.readerName + "</td>" +
            "<td>" + `<button type="button" onclick="remove(${t.readerId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.readerId})">Update</button>` +
            `<button type="button" onclick="books(${t.readerId})">Books</button>` +
            "</td>" +
            "</tr>";
    });
}

function showupdate(id) {
    document.getElementById('readernametoupdate').value = readers.find(t => t['readerId'] == id)['readername'];
    document.getElementById('updateformdiv').style.display = 'flex';
    readerIdToUpdate = id;
}

function remove(id) {
    fetch('http://localhost:23125/reader/' + id, {
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
    fetch('http://localhost:23125/reader', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { readerName: name, readerId: readerIdToUpdate })
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
    fetch('http://localhost:23125/reader', {
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

function books() {
    document.getElementById('noncrud_area').innerHTML = "";
    fetch('http://localhost:23125/OwnedBooks/OwnedBooks/')
        .then(response => response.json())
        .then(data => {
            console.log('Success:', data);
            data.forEach(t => {
                document.getElementById('noncrud_area').innerHTML += "<tr><td>" + t.title + "</td></tr>";
            })
        })
        .catch((error) => { console.error('Error:', error); });
}

function authors_books() {
    document.getElementById('noncrud_area').innerHTML = "";
    fetch('http://localhost:23125/OwnedBooks/OwnedBooks/')
        .then(response => response.json())
        .then(data => {
            console.log('Success:', data);
            data.forEach(t => {
                document.getElementById('noncrud_area').innerHTML += "<tr><td>" + t.name + "</td><td>"+t.bookCount+"</td></tr>";
            })
        })
        .catch((error) => { console.error('Error:', error); });
}