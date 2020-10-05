using System;
using NXOpen;

public class Program
{
    const string CALLBACK_NAME = "ESC_RESET";
    //------------------------------------------------------------------------------
    //  NX Startup
    //      This entry point activates the application at NX startup

    //Will work when complete path of the dll is provided to Environment Variable 
    //USER_STARTUP or USER_DEFAULT

    //OR

    //Will also work if dll is at folder named "startup" under any folder listed in the 
    //text file pointed to by the environment variable UGII_CUSTOM_DIRECTORY_FILE.
    //------------------------------------------------------------------------------
    public static int Startup()
    {
        int retValue = 0;
        var session = Session.GetSession();
        var theUI = UI.GetUI();
        try
        {

            session.LogFile.WriteLine($"Try to register new callback {CALLBACK_NAME}");

            theUI.MenuBarManager.AddMenuAction(CALLBACK_NAME,
                (x) => {
                    UI.GetUI().SelectionManager.ClearGlobalSelectionList();
                    return NXOpen.MenuBar.MenuBarManager.CallbackStatus.Continue;
                });


        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----            
            theUI.NXMessageBox.Show("UI Styler", NXMessageBox.DialogType.Error, ex.Message);

            session.LogFile.WriteLine(ex.Message);
            session.LogFile.WriteLine(ex.StackTrace);
        }
        return retValue;
    }


    public static int GetUnloadOption(string arg)
    {
        //Unloads the image explicitly, via an unload dialog
        //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);

        //Unloads the image immediately after execution within NX
        // return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);

        //Unloads the image when the NX session terminates
        return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
    }

}

