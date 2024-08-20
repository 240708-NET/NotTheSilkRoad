const getData = async () => {
const response = await fetch("http://localhost:5224")
if (!response.ok) {
    throw new Error(`Response status: ${response.status}`);
  }

const json = await response.json()

console.log(json)

}

getData();

