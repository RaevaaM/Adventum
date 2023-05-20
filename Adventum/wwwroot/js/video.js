//selecting li
const filter = document.querySelector(".gallery");

//selecting videobox
const itemboxes = document.querySelectorAll(".video");
console.log(itemboxes);

filter.addEventListener("click", (e) => {
    console.log(e)

    if (e.target.classList.contains("filter")) {
        filter.querySelector(".active").classList.remove("active");

        e.target.classList.add("active");

        const filterValue = e.target.getAttribute("data-filter");
        console.log(filterValue);

        itemboxes.forEach(item => {
            if (item.classList.contains(filterValue) || filterValue === "all") {
                item.classList.add("show");
                item.classList.remove("hide");
            }
            else {
                item.classList.add("hide");
            }
        })
    }
})

//video poping up
document.querySelectorAll('.video-container video').forEach(vid => {
    vid.onclick = () => {
        document.querySelector('.popup-video').style.display = "block";
        document.querySelector('.popup-video video').src = vid.getAttribute('src');

    }

});

document.querySelector('.popup-video span').onclick = () => {
    document.querySelector('.popup-video').style.display = "none";
}