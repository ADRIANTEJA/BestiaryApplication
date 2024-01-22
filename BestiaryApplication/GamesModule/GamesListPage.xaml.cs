using BestiaryApplication.Common.SharedControls.Windows;
using BestiaryApplication.Common.Resources.Assets.Cursors;
using BestiaryApplication.Common.utils;
using BestiaryApplication.GamesModule.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BestiaryApplication.GamesModule
{
    public partial class GamesListPage : Page
    {
        private string currentGameName;

        public GamesListPage()
        {
            InitializeComponent();
        }

        private void SetRPGGenre(object sender, RoutedEventArgs e)
        {
            genre_selector.Tag = "RPG";
        }

        private void SetActionRPGGenre(object sender, RoutedEventArgs e)
        {
            genre_selector.Tag = "Action-RPG";
        }

        private void OnChangeImageClick(object sender, RoutedEventArgs e)
        {
            save_game_changes_button.Visibility = Visibility.Visible;
            cancel_changes_button.Visibility = Visibility.Visible;
            delete_button.Visibility = Visibility.Hidden;
            currentGameName = game_name_modifier_field.Text;
        }

        private void OnSaveChangesClick(object sender, RoutedEventArgs e)
        {
            save_changes_yes_button.Visibility = Visibility.Visible; 
            save_game_changes_button.Visibility = Visibility.Hidden; 
            cancel_changes_button.Visibility = Visibility.Hidden;
            ShowPromptComponents();
        }

        private void OnSaveChangesYesClick(object sender, RoutedEventArgs e)
        {
            HidePromptComponents();
        }

        private void OnConfirmationNoClick(object sender, RoutedEventArgs e)
        {
            delete_game_yes_button.Visibility = Visibility.Hidden;
            HidePromptComponents();
            game_name_modifier_field.Text = currentGameName;
        }

        private void OnGameNameEnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Keyboard.ClearFocus();

                if (currentGameName != game_name_modifier_field.Text)
                {
                    CheckFieldValidStatus();

                    save_game_changes_button.Visibility = Visibility.Visible;
                    games_list_view_input_blocker.Visibility = Visibility.Visible;
                    cancel_changes_button.Visibility = Visibility.Visible;
                    delete_button.Visibility = Visibility.Hidden;
                    game_search_bar.IsEnabled = false;
                }
            }
        }

        private void OnNameFieldFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            currentGameName = game_name_modifier_field.Text;
        }

        private void OnNameFieldLostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            CheckFieldValidStatus();
        }

        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            delete_button.Visibility= Visibility.Hidden;
            delete_game_yes_button.Visibility = Visibility.Visible;
            ShowPromptComponents();
        }

        private void OnDeleteGameYesClick(object sender, RoutedEventArgs e)
        {
            delete_game_yes_button.Visibility = Visibility.Hidden;
            confirmation_no_button.Visibility = Visibility.Hidden;
            change_game_image_button.IsEnabled = true;
            game_name_modifier_field.IsEnabled = true;
            empty_name_field_warning.Visibility = Visibility.Hidden;
            HandleUIComponents();
        }

        private void OnCancelChangesClick(object sender, RoutedEventArgs e)
        {
            cancel_changes_button.Visibility = Visibility.Hidden;
            save_game_changes_button.Visibility = Visibility.Hidden;
            HandleUIComponents();
            game_name_modifier_field.Text = currentGameName;
        }

        private void OnAddGameClick(object sender, RoutedEventArgs e)
        {
            add_game_confirmation_Yes_button.Visibility = Visibility.Visible;
            add_game_confirmation_no_button.Visibility = Visibility.Visible;
            add_game_button.Visibility = Visibility.Hidden;
            new_game_name_field.IsEnabled = false;
            add_new_game_image_button.IsEnabled = false;
            genre_selector.IsExpanded = false;
            genre_selector.IsEnabled = false;
        }

        private void OnAddGameYesClick(object sender, RoutedEventArgs e)
        {
            add_game_button.Visibility = Visibility.Visible;
            add_game_confirmation_Yes_button.Visibility = Visibility.Hidden;
            add_game_confirmation_no_button.Visibility = Visibility.Hidden;
            new_game_name_field.Text = "";
            new_game_name_field.IsEnabled = true;
            add_new_game_image_button.IsEnabled = true;
            genre_selector.IsEnabled = true;
        }

        private void OnAddGameNoClick(object sender, RoutedEventArgs e)
        {
            add_game_button.Visibility = Visibility.Visible;
            add_game_confirmation_Yes_button.Visibility = Visibility.Hidden;
            add_game_confirmation_no_button.Visibility = Visibility.Hidden;
            new_game_name_field.IsEnabled = true;
            add_new_game_image_button.IsEnabled = true;
            genre_selector.IsEnabled = true;
        }

        private void OnNewGameNameEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !string.IsNullOrEmpty(new_game_name_field.Text)) Keyboard.ClearFocus();
        }

        private void OnNewGameNameFieldLostFocus(object sender, RoutedEventArgs e)
        {
            if (new_game_image.Source != null) add_game_button.IsEnabled = true;
        }

        private void OnNewGameImageClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(new_game_name_field.Text)) add_game_button.IsEnabled = true;
        }

        private void OnGenreSelectorLostFocus(object sender, RoutedEventArgs e)
        {
            genre_selector.IsExpanded = false;
        }

        private void ShowPromptComponents()
        {
            confirmation_prompt.Visibility = Visibility.Visible;
            confirmation_no_button.Visibility = Visibility.Visible;
            games_list_view_input_blocker.Visibility = Visibility.Visible;
            change_game_image_button.IsEnabled = false;
            game_name_modifier_field.IsEnabled = false;
            game_search_bar.IsEnabled = false;   
        }

        private void HidePromptComponents()
        {
            save_changes_yes_button.Visibility = Visibility.Hidden;
            empty_name_field_warning.Visibility = Visibility.Hidden;
            confirmation_no_button.Visibility = Visibility.Hidden;
            change_game_image_button.IsEnabled = true;
            game_name_modifier_field.IsEnabled = true;
            HandleUIComponents();
        }

        private void HandleUIComponents()
        {
            delete_button.Visibility = Visibility.Visible;
            confirmation_prompt.Visibility = Visibility.Hidden;
            games_list_view_input_blocker.Visibility = Visibility.Hidden;
            game_search_bar.IsEnabled = true;
        }

        private void CheckFieldValidStatus()
        {
            if (string.IsNullOrEmpty(game_name_modifier_field.Text)) empty_name_field_warning.Visibility = Visibility.Visible;
            else empty_name_field_warning.Visibility = Visibility.Hidden;
        }

        private void OnGameImageMouseEnter(object sender, MouseEventArgs e)
        {
            game_image.Cursor = CustomCursors.ZoomCursor;
            new_game_image.Cursor = CustomCursors.ZoomCursor;
        }

        private void OnImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            var image = (Image)sender;

            if (image.Source != null) GeneralMethods.ZoomImage(image.Source);
        }
    }
}
