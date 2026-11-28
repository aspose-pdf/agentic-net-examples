using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;                     // Needed for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;               // for the Form facade
using Aspose.Pdf.Forms;                // for field types (TextBoxField, CheckboxField, RadioButtonField)

// Alias the ambiguous Form class to avoid CS0104 errors.
using PdfFormFacade = Aspose.Pdf.Facades.Form;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "colors.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPdfPath);

        // Bind the document to the Form facade (Aspose.Pdf.Facades.Form).
        using (PdfFormFacade formFacade = new PdfFormFacade(doc))
        {
            // Get all field names from the facade.
            string[] fieldNames = formFacade.FieldNames;

            var lines = new List<string>();

            foreach (string name in fieldNames)
            {
                // Find the corresponding Field object in the document's Form collection.
                Field field = doc.Form?.Fields?.FirstOrDefault(f => f.Name == name);
                string colorInfo = "None";

                if (field != null)
                {
                    System.Drawing.Color? sysColor = null;

                    // Try to obtain a colour depending on the concrete field type.
                    switch (field)
                    {
                        case TextBoxField txt:
                            // Text colour is stored in DefaultAppearance.TextColor (System.Drawing.Color).
                            if (txt.DefaultAppearance != null)
                                sysColor = txt.DefaultAppearance.TextColor;
                            break;

                        case CheckboxField chk:
                            // Border colour is stored in Characteristics.Border (System.Drawing.Color).
                            sysColor = chk.Characteristics?.Border;
                            break;

                        case RadioButtonField rad:
                            sysColor = rad.Characteristics?.Border;
                            break;

                        default:
                            // Fallback: some fields expose an Aspose.Pdf.Color directly.
                            if (field.Color != null)
                            {
                                // Convert Aspose.Pdf.Color to System.Drawing.Color via ToRgb().
                                System.Drawing.Color rgb = field.Color.ToRgb();
                                colorInfo = $"{rgb.R},{rgb.G},{rgb.B}";
                            }
                            break;
                    }

                    if (sysColor.HasValue)
                    {
                        var c = sysColor.Value;
                        colorInfo = $"{c.R},{c.G},{c.B}";
                    }
                }

                lines.Add($"{name}: {colorInfo}");
            }

            // Write the collected field colour information to the text file.
            File.WriteAllLines(outputTxtPath, lines);
        }

        Console.WriteLine($"Field colours exported to '{outputTxtPath}'.");
    }
}
