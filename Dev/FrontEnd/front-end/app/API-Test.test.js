const getCategories = async () => {
    const response = await fetch("http://localhost:5224/category");
    const data = await response.json();
    console.log(data);
};

getCategories();