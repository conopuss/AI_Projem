namespace AI_Projem.Models
{
    public static class RecipeFilter
    {
        private static readonly string[] RecipeKeywords = new[]
  {
        "recipe", "ingredients", "cook", "meal", "dish", "food", "cuisine", "bake", "fry", "grill",
        "steam", "roast", "boil", "stew", "saute", "season", "mix", "blend", "serve", "eat",
        "soup", "salad", "dessert", "snack", "appetizer", "main course", "starter", "sauce",
        "spices", "herbs", "marinate", "knead", "chop", "slice", "dice", "peel", "whisk",

   
        "tomato", "cucumber", "onion", "garlic", "ginger", "carrot", "chicken", "beef", "lamb",
        "fish", "egg", "rice", "pasta", "potato", "pepper", "cheese", "bread", "flour", "sugar",
        "butter", "oil", "salt", "vinegar", "milk", "cream", "yogurt", "honey", "lemon", "mint",

 
        "tarif", "malzeme", "pişir", "yemek", "fırınla", "kızart", "haşla", "buharla", "ızgara",
        "sote", "kavur", "kaynat", "marine et", "yoğur", "karıştır", "doğra", "dilimle", "soy",
        "baharat", "otlar", "tatlandır", "sos", "tencere", "tava", "kepçe", "kaşık", "şef",

        "domates", "salatalık", "soğan", "sarımsak", "zencefil", "havuç", "tavuk", "et", "pirinç",
        "makarna", "patates", "biber", "peynir", "ekmek", "un", "şeker", "tereyağı", "zeytinyağı",
        "tuz", "sirke", "süt", "krema", "yoğurt", "bal", "limon", "nane", "yumurta", "balık", "meyve",
        "sebze", "çorba", "salata", "tatlı", "hamur", "mantı", "börek", "çörek", "dolma", "sarma"
    };

        public static bool IsRecipeQuery(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            input = input.ToLower();

            int matchCount = 0;
            foreach (var keyword in RecipeKeywords)
            {
                if (input.Contains(keyword.ToLower()))
                {
                    matchCount++;
                }
            }

           
            return matchCount >= 2;
        }
    }
}
