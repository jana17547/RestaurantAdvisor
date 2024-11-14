import { RestaurantView } from "./restaurantView.js";
import { RestaurantCard } from "./restaurantCard.js";
import { UserReview } from "./userReview.js";
import { Start } from "./start.js"

export class UserView {
	constructor(start, username) {
		this.container = null;
		this.username = username;
		this.start = start;
	}

	draw(input1User) {
		//console.log(input1User);
		this.container = document.createElement("div");
		this.container.className = "mainDiv2";
		document.body.appendChild(this.container);

		const botDiv2 = document.createElement("div");
		botDiv2.className = "botDiv2";
		this.container.appendChild(botDiv2);

		const backButton2 = document.createElement("button");
		backButton2.innerHTML = "BACK";
		backButton2.style.margin = "5px";
		backButton2.className = "btnyellow";
		backButton2.onclick = () => {
			//document.body.removeChild(this.container);
            this.container.innerHTML="";
            let start=new Start();
			start.draw(document.body);
		};
		this.container.appendChild(backButton2);

		const topDiv2 = document.createElement("div");
		topDiv2.className = "topDiv2";
		this.container.appendChild(topDiv2);
        
        const mainDivLR=document.createElement("div");
        mainDivLR.className="mainDivLR";
        topDiv2.appendChild(mainDivLR);

		const leftDiv2 = document.createElement("div");
		leftDiv2.className = "leftDiv2 ";
		mainDivLR.appendChild(leftDiv2);

		const searchDiv2 = document.createElement("div");
		searchDiv2.className = "searchDiv2 ui search";
		leftDiv2.appendChild(searchDiv2);

		const iconInput2 = document.createElement("select");
		iconInput2.className = "select1";
		iconInput2.id="select1";
		searchDiv2.appendChild(iconInput2);
        
		fetch("https://localhost:5001/Restaurant/GetTypeKitchen").then(
			p=>{
				p.json().then((typeK)=>{
					typeK.forEach(k=>{
						let op= document.createElement("option");
						op.innerHTML=k.type;
						op.value=k.type;
						iconInput2.appendChild(op);
						
					})
				})
			}
		)
        
		var br=document.createElement('br');
		searchDiv2.appendChild(br);
		const iconInput3 = document.createElement("select");
		iconInput3.className = "select2";
		iconInput3.id="select2";
		searchDiv2.appendChild(iconInput3);
        
		fetch("https://localhost:5001/Restaurant/GetCity").then(
			p=>{
				p.json().then((typeK)=>{
					typeK.forEach(k=>{
						let op= document.createElement("option");
						op.innerHTML=k.nameCity;
						op.value=k.nameCity;
						iconInput3.appendChild(op);
						
					})
				})
			}
		)
       
		
        br=document.createElement('br');
		searchDiv2.appendChild(br);
		const buttonSearch2 = document.createElement("button");
		buttonSearch2.innerHTML = "Search";
		buttonSearch2.className = "ui button green tiny";
		searchDiv2.appendChild(buttonSearch2);

		const results = document.createElement("div");
		results.className = "results2";
		leftDiv2.appendChild(results);


		buttonSearch2.onclick = () => {
			let type= document.getElementById('select1').value;
			console.log(type);

			let city= document.getElementById('select2').value;
			console.log(city);
			// spustiti input u lowercase
			/*var tag = this.container
				.querySelector(".inputSearch2")
				.value.toLowerCase();
			console.log(tag);*/
			fetch("https://localhost:5001/Restaurant/GetRestaurantType1/" + city+"/"+ type).then(
				(p) => {
					p.json().then((bus) => {
						results.innerHTML = "";
						bus.forEach((b) => {
							const restaurant = new RestaurantCard(
								b.name,
								b.address,
								b.contact,
								b.description,
								b.img,
								b.rating
								
							);
							let restaurantCardDiv2 = document.createElement("div");
							restaurantCardDiv2.className = "restaurantCardDiv3";
							results.appendChild(restaurantCardDiv2);
							restaurant.draw(restaurantCardDiv2);

							restaurantCardDiv2.onclick = () => {
								document.body.removeChild(this.container);
								let u = new RestaurantView(this, restaurant.name, input1User);
								u.draw();
							};
						});
					});
				}
			);
		};


		const middleDiv2 = document.createElement("div");
		middleDiv2.className = "middleDiv2 ";
		mainDivLR.appendChild(middleDiv2);
        
        const h3Div=document.createElement("div");
        h3Div.className="h3Div";
        middleDiv2.appendChild(h3Div);

		const recommendedDiv2 = document.createElement("h3");
		recommendedDiv2.innerHTML = "Recommended";
		recommendedDiv2.className = "recommendedDiv2";
		h3Div.appendChild(recommendedDiv2);
       
        const divRestaurant=document.createElement("div");
        divRestaurant.className="divRestaurant";
        middleDiv2.appendChild(divRestaurant);
		fetch(
			"https://localhost:5001/Restaurant/GetRecommended/" + input1User
		).then((p) => {
			p.json().then((bus) => {
				bus.forEach((b) => {
					const restaurant = new RestaurantCard(
						b.name,
						b.address,
						b.contact,
						b.description,
						b.img,
						b.rating
					);
					let restaurantCardDiv2 = document.createElement("div");
					restaurantCardDiv2.className = "restaurantCardDiv2";
					divRestaurant.appendChild(restaurantCardDiv2);
					restaurant.draw(restaurantCardDiv2);

                    restaurantCardDiv2.onclick = () => {
						document.body.removeChild(this.container);
						let u = new RestaurantView(this, restaurant.name, input1User);
						u.draw();
					};
				});
			});
		});
        
		const rightDiv2 = document.createElement("div");
		rightDiv2.className = "rightDiv2 ";
		middleDiv2.appendChild(rightDiv2);
        
        const h3Div1=document.createElement("div");
        h3Div1.className="h3Div1";
        rightDiv2.appendChild(h3Div1);

		const reviewsTitle = document.createElement("h3");
		reviewsTitle.innerHTML = "Reviews";
		reviewsTitle.className = "reviewsTitle2";
		h3Div1.appendChild(reviewsTitle);

        const reviewDiv=document.createElement("div");
        reviewDiv.className="reviewDiv";
        rightDiv2.appendChild(reviewDiv);
		this.drawReview(reviewDiv,input1User);
	}

    drawReview(reviewDiv,input1User)
	{
		fetch(
			"https://localhost:5001/Restaurant/GetUsernameReviews/" + input1User
		).then((p) => {
			p.json().then((bus) => {
				bus.forEach((b) => {
					const rev = new UserReview(b.name, b.review, b.rating, input1User);
					let reviewCard = document.createElement("div");
					reviewCard.className = "reviewCard";
					reviewDiv.appendChild(reviewCard);
					rev.draw(reviewCard);
				});
			});
		});
	}
}
