using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // needed for AnnotationActionCollection

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "secured_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields and clear any attached JavaScript actions
            foreach (Field field in doc.Form.Fields)
            {
                // Field.Actions may be null if no actions are defined
                if (field.Actions != null)
                {
                    // Clear all supported JavaScript action properties
                    field.Actions.OnEnter            = null;
                    field.Actions.OnExit             = null;
                    field.Actions.OnPressMouseBtn    = null;
                    field.Actions.OnReleaseMouseBtn  = null;
                    field.Actions.OnReceiveFocus     = null;
                    field.Actions.OnLostFocus        = null;
                    field.Actions.OnOpenPage         = null;
                    field.Actions.OnClosePage        = null;
                    field.Actions.OnShowPage         = null;
                    field.Actions.OnHidePage         = null;
                    field.Actions.OnModifyCharacter  = null;
                    field.Actions.OnValidate         = null;
                    field.Actions.OnFormat           = null;
                    field.Actions.OnCalculate        = null;
                }
            }

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved without JavaScript actions to '{outputPath}'.");
    }
}
