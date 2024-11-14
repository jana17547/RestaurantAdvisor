import { UserView } from "./userView.js";

export class UserReview {
	constructor(name, review, rating, username = null) {
		this.name = name;
		this.review = review;
		this.rating = rating;
		this.username = username;
	}

	draw(host) {
		const mainDiv = document.createElement("div");
		mainDiv.className = "userReviewCard ui segment";
		mainDiv.style.width = window.innerWidth * 0.25;
		host.appendChild(mainDiv);

		const name = document.createElement("div");
		name.innerHTML = this.name;
		name.className="nameRes";
		mainDiv.appendChild(name);

		const review = document.createElement("div");
		review.innerHTML = this.review;
		review.className = "ui segment reviewContent2";
		mainDiv.appendChild(review);

		const bottom = document.createElement("div");
		bottom.className = "userReviewBottom";
		mainDiv.appendChild(bottom);
		
		const updateDiv=document.createElement("div");
		updateDiv.className="updateDiv";
		mainDiv.appendChild(updateDiv);

		const rating = document.createElement("div");
		rating.innerHTML = "⭐  " + this.rating;
		bottom.appendChild(rating);

		const b = document.body.querySelector(".bigDiv3");

		const deleteButton = document.createElement("button");
		deleteButton.innerHTML = "DELETE";
		deleteButton.className = "ui button red tiny";
		if (b != null) deleteButton.className = "hidden2";

		deleteButton.onclick = () => {
			if (b == null) {
				fetch(
					`https://localhost:5001/Restaurant/DeleteReview/${this.username}&${this.name}`,
					{
						method: "DELETE",
						headers: { "Content-Type": "application/json" },
					}
				).then((p) => {
					host.removeChild(mainDiv);
				});
			}
		};
		bottom.appendChild(deleteButton);
        
		const updateButton=document.createElement("button");
		updateButton.innerHTML="UPDATE";
		updateButton.className="ui button yellow tiny";
		updateButton.onclick=(ev)=>this.update(updateDiv,this.username,this.name,review);
	    bottom.appendChild(updateButton);
    }

	update(updateDiv,username,name,review)
	{
		console.log(review);
       let meniDiv=document.createElement("div");
	   meniDiv.className="meniDiv";
	   updateDiv.appendChild(meniDiv);

	   let lbl=document.createElement("label");
	   lbl.innerHTML="Make changes:";
	   meniDiv.appendChild(lbl);
	  
	   let input1=document.createElement("input");
	   input1.type="text";
	   meniDiv.appendChild(input1);

	   let btnSave=document.createElement("button");
	   btnSave.innerHTML="SAVE";
	   btnSave.className="ui button green tiny";
	   meniDiv.appendChild(btnSave);
       
	   btnSave.onclick=(ev)=>{
		fetch("https://localhost:5001/Restaurant/UpdateReview/"+username+"/"+name+"/"+input1.value,{
			method:"PUT"
		}).then(data=>{
			data.json().then(info=>{
				this.review=info.review;
				review.innerHTML=JSON.stringify(input1.value);
				meniDiv.innerHTML="";
			})
		})
	   }
	
	}
}
