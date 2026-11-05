using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "secured.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Iterate over all form fields and clear any JavaScript actions
                foreach (Field field in doc.Form)
                {
                    ClearFieldActions(field);
                }

                // Save the PDF without JavaScript actions
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved without JavaScript actions to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Generic clearing for any field that derives from Annotation
    static void ClearFieldActions(Field field)
    {
        if (field?.Actions == null)
            return;

        var actions = field.Actions;
        // Set every supported action property to null (removing JavaScript)
        actions.OnEnter = null;
        actions.OnExit = null;
        actions.OnPressMouseBtn = null;
        actions.OnReleaseMouseBtn = null;
        actions.OnReceiveFocus = null;
        actions.OnLostFocus = null;
        actions.OnOpenPage = null;
        actions.OnClosePage = null;
        actions.OnShowPage = null;
        actions.OnHidePage = null;
        actions.OnModifyCharacter = null;
        actions.OnValidate = null;
        actions.OnFormat = null;
        actions.OnCalculate = null;
    }
}
