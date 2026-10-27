using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Form field types and actions

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "secured_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdf = new Document(inputPath))
        {
            // Iterate over all form fields in the document
            foreach (Field field in pdf.Form)
            {
                // If the field has any JavaScript actions, clear them using the valid properties of AnnotationActionCollection
                if (field.Actions != null)
                {
                    field.Actions.OnEnter = null;
                    field.Actions.OnExit = null;
                    field.Actions.OnPressMouseBtn = null;
                    field.Actions.OnReleaseMouseBtn = null;
                    field.Actions.OnReceiveFocus = null;
                    field.Actions.OnLostFocus = null;
                    field.Actions.OnOpenPage = null;
                    field.Actions.OnClosePage = null;
                    field.Actions.OnShowPage = null;
                    field.Actions.OnHidePage = null;
                    field.Actions.OnModifyCharacter = null;
                    field.Actions.OnValidate = null;
                    field.Actions.OnFormat = null;
                    field.Actions.OnCalculate = null;
                }
            }

            // Save the cleaned PDF; no SaveOptions needed because the output is PDF
            pdf.Save(outputPath);
        }

        Console.WriteLine($"JavaScript actions removed. Saved to '{outputPath}'.");
    }
}
