namespace FlashStudy.Utilities
{
  class Help
  {
    public static string mainMenu = @"
Main Menu
Type 1 to Study
Type 2 to Manage Stacks
Type 3 to Manage FlashCards
Type 4 to View Study Sessions
Type 0 to Exit";

    public static string stackMessage = @"
Manage Stacks
* show: to view stacks
* add [stack name]: to add stacks
* edit [old stack name] [new stack name]: to edit stacks
* remove [stack name]: to remove stacks
* Back or 0: go back to the main menu";

    public static string cardMessage = @"
Manage Flash Cards
* show: to view cards
* add: to add a flash card
* remove [card id]: to remove a card
* Back or 0: go back to the main menu";
  }
}
