namespace Cookmate.Data
{
    public static class DataConstants
    {
        public class User
        {
            public const int FullNameMinLength = 5;
            public const int FullNameMaxLength = 40;
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;
        }

        public const int RecipeNameMin = 3;
        public const int RecipeNameMax = 40;
        public const int RecipeDescriptionMin = 10;

        public const int CategoryNameMax = 30;

        public const int MeasurementUnitMaxLength = 20;

        public const int IngredientNameMax = 50;
        public const int IngredientQuantityMax = 5;

        public const int CookingTimeMin = 3;
        public const int CookingTimeMax = 720;

        
    }
}
