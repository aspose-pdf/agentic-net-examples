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

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Iterate over all form fields and remove any JavaScript actions attached to them
        foreach (Field field in doc.Form.Fields)
        {
            var actions = field.Actions;
            if (actions == null) continue;

            // Set every possible action property to null – this effectively removes the JavaScript.
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

        // Save the cleaned PDF
        doc.Save(outputPath);

        Console.WriteLine($"All JavaScript actions removed. Output saved to '{outputPath}'.");
    }
}
