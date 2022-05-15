const fs = require("fs");


// const clientsFile = fs.readFileSync("./Employees.geojson");
// const clients = JSON.parse(clientsFile.toString());
// let id = 0;
// clients.features.forEach((feature) => {
// 	feature.properties.name_employee = 'test'+ id;
// 	feature.properties.lastname_employee = 'test'+ id;
// 	id++;
// });
// fs.writeFileSync("./Employees-formatted.geojson", JSON.stringify(clients));


const clientsFile = fs.readFileSync("./product.geojson");
const clients = JSON.parse(clientsFile.toString());
let id = 0;
clients.features.forEach((feature) => {
	feature.properties.product_name = 'test'+ id;
	feature.properties.price = id + Math.random();
	id++;
});
fs.writeFileSync("./product-formatted.geojson", JSON.stringify(clients));
