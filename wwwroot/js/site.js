// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.addEventListener("DOMContentLoaded", () => {
    var addRIButton = document.getElementById("form-recipe-ingredient-add-button");
    //abort early if element is not found (wrong page..)
    if (!addRIButton)
        return;

    addRIButton.addEventListener("click", () => {
        //get all recipe ingredient divs
        var lastRecipeIngredient = document.getElementsByClassName("form-recipe-ingredient");
        //select the last item in the HTMLCollection returned
        lastRecipeIngredient = lastRecipeIngredient[lastRecipeIngredient.length - 1];
        //get handle to parent div, so we can append to it
        var recipeIngredientList = lastRecipeIngredient.parentElement;

        //make a copy
        var newRecipeIngredient = lastRecipeIngredient.cloneNode(true);
        recipeIngredientList.append(newRecipeIngredient);
    });


    //When submitting the recipe-form, search for our specific keyword and replace it with proper indexing so ASP/controller will bind it correctly
    var submitButton = document.getElementById("form-create-recipe");
    submitButton.addEventListener("submit", () => {
        //get all recipe ingredient divs
        var allRecipeIngredients = document.getElementsByClassName("form-recipe-ingredient");

        for (var i = 0; i < allRecipeIngredients.length; i++) {
            //replace the special string ##index## with a ordered numbering so ModelBinder can bind for us automagically
            //allRecipeIngredients[i].innerHTML = allRecipeIngredients[i].innerHTML.replaceAll("##index##", i)
            for (j = 0; j < allRecipeIngredients[i].childElementCount; j++) {
                allRecipeIngredients[i].children[j].name = allRecipeIngredients[i].children[j].name.replaceAll("##index##", i);
            }
        }
    });

});

//function to remove this specific ingredient from recipe-form
//but we still need 1 so we can clone, also recipes cannot be empty
//button onClick is used to reach this function due to cloneNode ignoring eventListeners
function removeThisIngredient(deleteButton) {
    var ingredientsList = deleteButton.parentNode.parentNode;
    console.log("delete clicked");
    if (deleteButton.parentNode.parentNode.childElementCount <= 1) {
        alert("You need at least one ingredient!");
    } else {
        deleteButton.parentNode.remove();
    }
}


