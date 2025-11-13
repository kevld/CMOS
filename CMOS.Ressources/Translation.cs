namespace CMOS.Ressources
{
    public static class Translation
    {
        public const string SHELL_HALT = "shell.halt";
        public const string MENU_LABEL_COMMANDS = "menu.label.commands";
        public const string MENU_LABEL_PROGRAMS = "menu.label.programs";

        public const string SHELL_HELP_ABOUT = "shell.help.about";
        public const string SHELL_HELP_EXIT = "shell.help.exit";
        public const string SHELL_HELP_HELP = "shell.help.help";
        public const string SHELL_HELP_DEL = "shell.help.del";
        public const string SHELL_HELP_DEL_FILE = "shell.help.del.file";
        public const string SHELL_HELP_LIST_FILES = "shell.help.ls";
        public const string SHELL_HELP_PROGRAM_CLOCK = "shell.help.clock";
        public const string SHELL_HELP_PROGRAM_DISK = "shell.help.disk";
        public const string SHELL_HELP_PROGRAM_TEXT = "shell.help.text";
        public const string SHELL_HELP_PROGRAM_TODO = "shell.help.todo";
        public const string SHELL_HELP_PROGRAM_CONTROL_PANEL = "shell.help.ctl";
        public const string SHELL_INITIALIZE = "shell.init";

        public const string ERR_FILE_NOT_FOUND = "err.file.404";
        public const string ERR_FILE_NOT_SAVED = "err.file.not.saved";
        public const string ERR_SHELL_DEL_SYNTAX = "err.shell.del.syntax";
        public const string ERR_SHELL_UNKNOWN_COMMAND = "err.shell.unknown.command";
        public const string ERR_FILE_NOT_OPENED = "err.file.not.opened";

        public const string GENERIC_PROGRAM_HELP_ABOUT = "generic.program.help.about";
        public const string GENERIC_PROGRAM_HELP_EXIT = "generic.program.help.exit";
        public const string GENERIC_PROGRAM_HELP_HELP = "generic.program.help.help";
        public const string GENERIC_PROGRAM_HELP_LABEL = "generic.program.help.label";
        public const string GENERIC_PROGRAM_HELP_OPEN = "generic.program.help.open";
        public const string GENERIC_PROGRAM_HELP_SAVE = "generic.program.help.save";
        public const string GENERIC_PROGRAM_PRESS_ANY_KEY = "generic.program.press.key";
        public const string GENERIC_PROGRAM_OPTION_OR = "generic.program.option.or";
        public const string GENERIC_PROGRAM_FILE_NAME = "generic.program.file.name";
        public const string GENERIC_PROGRAM_FILE_SAVED = "generic.program.file.saved";

        public const string APP_DISK_LABEL_FREE_SPACE = "app.disk.lbl.fs";

        public const string APP_TEXT_APP_NAME = "app.text.name";
        public const string APP_TEXT_KEY_ARROW = "app.text.key.arrow";
        public const string APP_TEXT_KEY_ENTER = "app.text.key.enter";
        public const string APP_TEXT_KEY_BACKSPACE = "app.text.key.backspace";
        public const string APP_TEXT_EXIT_PRESS = "app.text.exit.press";
        public const string APP_TEXT_EXIT_PRESS_2 = "app.text.exit.press.2";
        public const string APP_TEXT_NEW_FILE = "app.text.new.file";
        public const string APP_TEXT_NO_FILE = "app.text.no.file";
        public const string APP_TEXT_OPEN_NAVIGATE = "app.text.open.navigate";

        public const string APP_TODO_APP_NAME = "app.todo.name";
        public const string APP_TODO_KEY_ARROWS = "app.todo.key.arrows";
        public const string APP_TODO_KEY_SPACE = "app.todo.key.space";
        public const string APP_TODO_KEY_ENTER = "app.todo.key.enter";

        public const string APP_CTL_LABEL_SELECT_LANG = "app.ctl.select.lg";
        public const string APP_CTL_CONFIG_SAVED = "app.ctl.cfg.saved";

        public static Dictionary<string, string> LoadEn()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            res.Add(SHELL_HALT, "Shutting down computer.");
            res.Add(MENU_LABEL_COMMANDS, "Commands");
            res.Add(MENU_LABEL_PROGRAMS, "Programs");

            res.Add(SHELL_HELP_ABOUT, "About this program");
            res.Add(SHELL_HELP_EXIT, "Shutdown the computer");
            res.Add(SHELL_HELP_HELP, "Display this menu");
            res.Add(SHELL_HELP_DEL, "Delete specified file");
            res.Add(SHELL_HELP_DEL_FILE, "file");
            res.Add(SHELL_HELP_LIST_FILES, "List files");
            res.Add(SHELL_HELP_PROGRAM_CLOCK, "Start the clock");
            res.Add(SHELL_HELP_PROGRAM_DISK, "Display disk data");
            res.Add(SHELL_HELP_PROGRAM_TEXT, "Start the text editor");
            res.Add(SHELL_HELP_PROGRAM_TODO, "Start the todo app");
            res.Add(SHELL_HELP_PROGRAM_CONTROL_PANEL, "Start the config app");
            res.Add(SHELL_INITIALIZE, "Type \"Help\" or \"?\" to display the help");

            res.Add(GENERIC_PROGRAM_HELP_ABOUT, "About");
            res.Add(GENERIC_PROGRAM_HELP_EXIT, "Exit");
            res.Add(GENERIC_PROGRAM_HELP_HELP, "Display this menu");
            res.Add(GENERIC_PROGRAM_HELP_LABEL, "Help");
            res.Add(GENERIC_PROGRAM_HELP_OPEN, "Open");
            res.Add(GENERIC_PROGRAM_HELP_SAVE, "Save");
            res.Add(GENERIC_PROGRAM_PRESS_ANY_KEY, "Press any key to continue...");
            res.Add(GENERIC_PROGRAM_OPTION_OR, "or");
            res.Add(GENERIC_PROGRAM_FILE_NAME, "File name");
            res.Add(GENERIC_PROGRAM_FILE_SAVED, "File saved");

            res.Add(APP_DISK_LABEL_FREE_SPACE, "Free space");
            
            res.Add(APP_CTL_LABEL_SELECT_LANG, "Select language : en / fr");
            res.Add(APP_CTL_CONFIG_SAVED, "Config saved.");
            
            res.Add(APP_TEXT_APP_NAME, "Text editor.");
            res.Add(APP_TEXT_KEY_ARROW, "Arrow : Move cursor");
            res.Add(APP_TEXT_KEY_ENTER, "Enter : New line");
            res.Add(APP_TEXT_KEY_BACKSPACE, "Backspace : Remove character");
            res.Add(APP_TEXT_EXIT_PRESS, "Press ");
            res.Add(APP_TEXT_EXIT_PRESS_2, " to exit without saving, or any key to go back.");
            res.Add(APP_TEXT_NEW_FILE, "New file");
            res.Add(APP_TEXT_NO_FILE, "No file found");
            res.Add(APP_TEXT_OPEN_NAVIGATE, "Up/Down : Navigate | Enter : Open | Esc : Cancel");

            res.Add(APP_TODO_APP_NAME, "Todo list");
            res.Add(APP_TODO_KEY_ARROWS, "Up/Down : Select todo task");
            res.Add(APP_TODO_KEY_SPACE, "Space : Complete task");
            res.Add(APP_TODO_KEY_ENTER, "Enter : New task");

            res.Add(ERR_FILE_NOT_FOUND, "This File does not exist");
            res.Add(ERR_SHELL_DEL_SYNTAX, "Syntax error. Usage : del <file name>");
            res.Add(ERR_SHELL_UNKNOWN_COMMAND, "Unknown command");
            res.Add(ERR_FILE_NOT_SAVED, "File not saved!");
            res.Add(ERR_FILE_NOT_OPENED, "Error while opening!");

            return res;
        }

        public static Dictionary<string, string> LoadFr()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            res.Add(SHELL_HALT, "Extinction de l'ordinateur.");
            res.Add(MENU_LABEL_COMMANDS, "Commandes.");
            res.Add(MENU_LABEL_PROGRAMS, "Programmes");

            res.Add(SHELL_HELP_ABOUT, "A propos de ce programme");
            res.Add(SHELL_HELP_EXIT, "Extinction de l'ordinateur");
            res.Add(SHELL_HELP_HELP, "Affiche ce menu");
            res.Add(SHELL_HELP_DEL, "Supprime le fichier spécifié");
            res.Add(SHELL_HELP_DEL_FILE, "fichier");
            res.Add(SHELL_HELP_LIST_FILES, "Lister les fichiers");
            res.Add(SHELL_HELP_PROGRAM_CLOCK, "Démarrer l'horloge");
            res.Add(SHELL_HELP_PROGRAM_DISK, "Afficher les informations du disque");
            res.Add(SHELL_HELP_PROGRAM_TEXT, "Ouvrir l'éditeur de texte");
            res.Add(SHELL_HELP_PROGRAM_TODO, "Ouvrir la liste de tâche à faire");
            res.Add(SHELL_HELP_PROGRAM_CONTROL_PANEL, "Ouvrir le panneau de configuration");
            res.Add(SHELL_INITIALIZE, "Taper \"Help\" ou \"?\" pour afficher cette aide");

            res.Add(GENERIC_PROGRAM_HELP_ABOUT, "A propos");
            res.Add(GENERIC_PROGRAM_HELP_EXIT, "Quitter");
            res.Add(GENERIC_PROGRAM_HELP_HELP, "Afficher ce menu");
            res.Add(GENERIC_PROGRAM_HELP_LABEL, "Aide");
            res.Add(GENERIC_PROGRAM_HELP_OPEN, "Ouvrir");
            res.Add(GENERIC_PROGRAM_HELP_SAVE, "Sauvegarder");
            res.Add(GENERIC_PROGRAM_PRESS_ANY_KEY, "Appuyer sur une touche pour continuer...");
            res.Add(GENERIC_PROGRAM_OPTION_OR, "ou");
            res.Add(GENERIC_PROGRAM_FILE_NAME, "Nom du fichier");
            res.Add(GENERIC_PROGRAM_FILE_SAVED, "Fichier sauvegardé");

            res.Add(APP_DISK_LABEL_FREE_SPACE, "Espace libre");

            res.Add(APP_CTL_LABEL_SELECT_LANG, "Sélectionner la langue : en / fr");
            res.Add(APP_CTL_CONFIG_SAVED, "Configuration sauvegardée.");

            res.Add(APP_TEXT_APP_NAME, "Editeur de texte.");
            res.Add(APP_TEXT_KEY_ARROW, "Flèches : Déplacer le curseur");
            res.Add(APP_TEXT_KEY_ENTER, "Entrée : Nouvelle ligne");
            res.Add(APP_TEXT_KEY_BACKSPACE, "Retour arrière : Effacer caractère");
            res.Add(APP_TEXT_EXIT_PRESS, "Appuyer sur ");
            res.Add(APP_TEXT_EXIT_PRESS_2, " pour quitter sans sauvegarder, ou n'importe quelle autre touche pour continuer.");
            res.Add(APP_TEXT_NEW_FILE, "Nouveau fichier");
            res.Add(APP_TEXT_NO_FILE, "Aucun fichier trouvé");
            res.Add(APP_TEXT_OPEN_NAVIGATE, "Haut/Bas : Sélectionner | Entrée : Ouvrir | Echap : Annuler");

            res.Add(APP_TODO_APP_NAME, "Liste de tâches");
            res.Add(APP_TODO_KEY_ARROWS, "Haut/Bas : Sélectionner la tâche");
            res.Add(APP_TODO_KEY_SPACE, "Space : Terminer la tâche");
            res.Add(APP_TODO_KEY_ENTER, "Enter : Nouvelle tâche");

            res.Add(ERR_FILE_NOT_FOUND, "Ce fichier n'existe pas");
            res.Add(ERR_SHELL_DEL_SYNTAX, "Erreur de syntaxe. Utiliser : del <nom du fichier>");
            res.Add(ERR_SHELL_UNKNOWN_COMMAND, "Commande inconnue");
            res.Add(ERR_FILE_NOT_SAVED, "Fichier non sauvegardé!");
            res.Add(ERR_FILE_NOT_OPENED, "Error while opening!");

            return res;
        }
    }
}
