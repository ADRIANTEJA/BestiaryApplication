using BestiaryApplication.Common.Resources.Assets.Cursors;
using BestiaryApplication.Common.utils;
using BestiaryApplication.CreatureModule.ViewModel;
using BestiaryApplication.MainModule.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace BestiaryApplication.CreatureModule
{
    /// <summary>
    /// Interaction logic for CreatureListPage.xaml
    /// </summary>
    public partial class CreatureListPage : Page
    {
        private string? currentCreatureName;
        private string? currentCreatureType;
        private string  currentCreatureElement = "Water";
        private string? currentCreatureStrongPoint;
        private string? currentCreatureWeakPoint;
        private string? currentCreatureDescription;

        public CreatureListPage()
        {
            InitializeComponent();
        }

        private void OnAddCreatureClick(object sender, RoutedEventArgs e)
        {
            add_creature_button.Visibility = Visibility.Hidden;
            add_creature_yes_button.Visibility = Visibility.Visible;
            add_creature_no_button.Visibility = Visibility.Visible;
            add_creature_image_button.IsEnabled = false;
            creature_game_selector.IsExpanded = false;
            creature_game_selector.IsEnabled = false;
            element_selector.IsExpanded = false;
            element_selector.IsEnabled = false;
            name_field.IsEnabled = false;
            type_field.IsEnabled = false;
            weak_point_field.IsEnabled = false;
            strong_point_field.IsEnabled = false;
            description_field.IsEnabled = false;
        }

        private void OnAddCreatureYesClick(object sender, RoutedEventArgs e)
        {
            add_creature_button.Visibility = Visibility.Visible;
            add_creature_no_button.Visibility = Visibility.Hidden;
            add_creature_yes_button.Visibility = Visibility.Hidden;
            add_creature_image_button.IsEnabled = true;
            creature_game_selector.IsEnabled = true;
            element_selector.IsEnabled = true;
            name_field.IsEnabled = true;
            type_field.IsEnabled = true;
            weak_point_field.IsEnabled = true;
            strong_point_field.IsEnabled = true;
            description_field.IsEnabled = true;
        }

        private void OnAddCreatureNoButton(object sender, RoutedEventArgs e)
        {
            add_creature_button.Visibility = Visibility.Visible;
            add_creature_no_button.Visibility = Visibility.Hidden;
            add_creature_yes_button.Visibility = Visibility.Hidden;
            add_creature_image_button.IsEnabled = true;
            creature_game_selector.IsEnabled = true;
            element_selector.IsEnabled = true;
            name_field.IsEnabled = true;
            type_field.IsEnabled = true;
            weak_point_field.IsEnabled = true;
            strong_point_field.IsEnabled = true;
            description_field.IsEnabled = true;
        }

        private void OnSaveChangesClick(object sender, RoutedEventArgs e)
        {
            save_changes_button.Visibility = Visibility.Hidden;
            save_changes_yes_button.Visibility = Visibility.Visible;
            cancel_changes_button.Visibility = Visibility.Hidden;
            ShowPromptComponents();
            creatures_list_view_input_blocker.Visibility = Visibility.Visible;
        }

        private void OnSaveChangesYesClick(object sender, RoutedEventArgs e)
        {
            save_changes_yes_button.Visibility = Visibility.Hidden;
            HidePromptComponents();
        }

        private void OnConfirmationNoClick(object sender, RoutedEventArgs e)
        {
            delete_creature_yes_button.Visibility = Visibility.Hidden;
            save_changes_yes_button.Visibility = Visibility.Hidden;
            delete_creature_yes_button.Visibility = Visibility.Hidden;
            HidePromptComponents();
            PreserveFieldsValue();
        }

        private void OnCancelChangesClick(object sender, RoutedEventArgs e)
        {
            cancel_changes_button.Visibility = Visibility.Hidden;
            save_changes_button.Visibility = Visibility.Hidden;
            HandleUIComponents();
            PreserveFieldsValue();
        }

        private void OnDeleteCreatureClick(object sender, RoutedEventArgs e)
        {
            delete_creature_yes_button.Visibility = Visibility.Visible;
            delete_creature_button.Visibility = Visibility.Hidden;
            ShowPromptComponents();
        }

        private void OnDeleteCreatureYesClick(object sender, RoutedEventArgs e)
        {
            delete_creature_yes_button.Visibility = Visibility.Hidden;
            HidePromptComponents();
        }

        private void OnEditElementsExpanded(object sender, RoutedEventArgs e)
        {
            edit_strong_point_field.Visibility = Visibility.Hidden;
            edit_weak_point_field.Visibility = Visibility.Hidden;
        }

        private void OnEditElementsCollapsed(object sender, RoutedEventArgs e)
        {
            edit_strong_point_field.Visibility = Visibility.Visible;
            edit_weak_point_field.Visibility = Visibility.Visible;
        }

        private void OnEditNameFieldFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            currentCreatureName = edit_name_field.Text;
        }

        private void OnEditNameFieldLostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            CheckField(edit_name_field.Text, "Name field cannot be empty");
        }

        private void OnEditTypeFieldFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            currentCreatureType = edit_type_field.Text;
        }

        private void OnEditTypeFieldLostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            CheckField(edit_type_field.Text, "Type field cannot be empty");
        }

        private void OnEditElementClick(object sender, EventArgs e)
        {
            edit_element_selector_header.Text = edit_element_list.SelectedValue.ToString();
            currentCreatureElement = edit_element_list.SelectedValue.ToString()!;
            edit_element_selector.IsExpanded = false;
        }

        private void OnEditSPFieldFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            currentCreatureStrongPoint = edit_strong_point_field.Text;
        }

        private void OnEditSPFieldLostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            CheckField(edit_strong_point_field.Text, "Strong point field cannot be empty");
        }

        private void OnEditWPFieldFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            currentCreatureWeakPoint = edit_weak_point_field.Text;
        }

        private void OnEditWPFieldLostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            CheckField(edit_weak_point_field.Text, "Weak point field cannot be empty");
        }

        private void OnEditDescriptionFieldFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            currentCreatureDescription = edit_description_field.Text;
        }

        private void OnEditDescriptionFieldLostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            CheckField(edit_description_field.Text, "Description field cannot be empty");
        }

        private void OnEditNameEnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Keyboard.ClearFocus();

                if (currentCreatureName != edit_name_field.Text) ValidateFieldStatus(edit_name_field.Text,
                                                                                    "Name field cannot be empty");
            }
        }

        private void OnEditTypeEnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Keyboard.ClearFocus();

                if (currentCreatureType != edit_type_field.Text) ValidateFieldStatus(edit_type_field.Text,
                                                                                     "Type cannot be empty");
            }
        }

        private void OnElementEdited(object sender, SelectionChangedEventArgs e)
        {
            if (edit_element_list.SelectedValue != null
                && edit_element_list.SelectedValue.ToString() != currentCreatureElement)
            {
                edit_element_selector_header.Text = edit_element_list.SelectedValue.ToString();
                delete_creature_button.Visibility = Visibility.Hidden;
                save_changes_button.Visibility = Visibility.Visible;
                cancel_changes_button.Visibility = Visibility.Visible;
                creatures_list_view_input_blocker.Visibility = Visibility.Visible;

            }
            edit_element_selector.IsExpanded = false;
        }

        private void OnElementChanged(object sender, SelectionChangedEventArgs e)
        {
            if (element_list.SelectedValue != null)
            {
                element_selector_header.Text = element_list.SelectedValue.ToString();
            }
            element_selector.IsExpanded = false;
        }

        private void OnEditCreatureImageClick(object sender, RoutedEventArgs e)
        {
            save_changes_button.Visibility = Visibility.Visible;
            cancel_changes_button.Visibility = Visibility.Visible;
            delete_creature_button.Visibility = Visibility.Hidden;
        }

        private void OnEditSPEnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Keyboard.ClearFocus();

                if (currentCreatureStrongPoint != edit_strong_point_field.Text) ValidateFieldStatus(edit_strong_point_field.Text,
                                                                                               "Strong point field cannot be empty");
            }
        }

        private void OnEditWPEnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Keyboard.ClearFocus();

                if (currentCreatureWeakPoint != edit_weak_point_field.Text) ValidateFieldStatus(edit_weak_point_field.Text,
                                                                                          "Weak point field cannot be empty");
            }
        }

        private void OnEditDescEnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Keyboard.ClearFocus();

                if (currentCreatureDescription != edit_description_field.Text) ValidateFieldStatus(edit_description_field.Text,
                                                                                              "Description field cannot be Empty");
            }
        }

        private void ShowPromptComponents()
        {
            confirmation_prompt.Visibility = Visibility.Visible;
            confirmation_no_button.Visibility = Visibility.Visible;
            creatures_list_view_input_blocker.Visibility = Visibility.Visible;
            edit_name_field.IsEnabled = false;
            edit_type_field.IsEnabled = false;
            edit_element_selector.IsExpanded = false;
            edit_element_selector.IsEnabled = false;
            edit_strong_point_field.IsEnabled = false;
            edit_weak_point_field.IsEnabled = false;
            edit_description_field.IsEnabled = false;
            change_creature_image_button.IsEnabled = false;
            creature_search_bar.IsEnabled = false;
        }

        private void HidePromptComponents()
        {
            save_changes_button.Visibility = Visibility.Hidden;
            empty_field_warning.Visibility = Visibility.Hidden;
            confirmation_no_button.Visibility = Visibility.Hidden;
            edit_name_field.IsEnabled = true;
            edit_type_field.IsEnabled = true;
            edit_element_selector.IsEnabled = true;
            edit_strong_point_field.IsEnabled = true;
            edit_weak_point_field.IsEnabled = true;
            edit_description_field.IsEnabled = true;
            change_creature_image_button.IsEnabled = true;
            HandleUIComponents();
        }

        private void HandleUIComponents()
        {
            delete_creature_button.Visibility = Visibility.Visible;
            confirmation_prompt.Visibility = Visibility.Hidden;
            creatures_list_view_input_blocker.Visibility = Visibility.Hidden;
            creature_search_bar.IsEnabled = true;
        }

        private void PreserveFieldsValue()
        {
            edit_name_field.Text = currentCreatureName;
            edit_type_field.Text = currentCreatureType;
            edit_strong_point_field.Text = currentCreatureStrongPoint;
            edit_weak_point_field.Text = currentCreatureWeakPoint;
            edit_description_field.Text = currentCreatureDescription;
        }

        private void ValidateFieldStatus(string editFieldData, string warningText)
        {
            CheckField(editFieldData, warningText);
            
            save_changes_button.Visibility = Visibility.Visible;
            cancel_changes_button.Visibility = Visibility.Visible;
            creatures_list_view_input_blocker.Visibility = Visibility.Visible;
            delete_creature_button.Visibility = Visibility.Hidden;
        }

        private void CheckField(string editFieldData, string warningText)
        {
            if (string.IsNullOrEmpty(editFieldData))
            {
                empty_field_warning.Text = warningText;
                empty_field_warning.Visibility = Visibility.Visible;
            }
            else empty_field_warning.Visibility = Visibility.Hidden;
        }

        private void OnImageMouseEnter(object sender, MouseEventArgs e)
        {
            new_creature_image.Cursor = CustomCursors.ZoomCursor;
            creature_game_image.Cursor = CustomCursors.ZoomCursor;
            creature_image.Cursor = CustomCursors.ZoomCursor;
        }

        private void OnCreatureImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            var image = (System.Windows.Controls.Image)sender;

            if (image.Source != null) GeneralMethods.ZoomImage(image.Source);
        }

        private void OnCreaturePageUnloaded(object sender, RoutedEventArgs e)
        {

            var viewModel = (CreatureViewModel)main_page_container.DataContext;
            viewModel?.InsertLastConsultedCreatureCommand.Execute(viewModel.SelectedCreature);
        }
    }
}
