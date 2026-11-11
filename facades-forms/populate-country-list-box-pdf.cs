using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path for the PDF that will contain the list box
        const string pdfPath = "CountryList.pdf";

        // Create an empty PDF with a single page
        using (Document doc = new Document())
        {
            // Add a blank page (size A4)
            doc.Pages.Add();
            doc.Save(pdfPath);
        }

        // Initialize the FormEditor to work with the PDF
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(pdfPath);

            // Define the rectangle where the list box will appear (llx, lly, urx, ury)
            // Aspose.Pdf.Rectangle stores coordinates as double, but FormEditor.AddField expects float.
            // Cast each coordinate to float to satisfy the overload.
            Rectangle listBoxRect = new Rectangle(100, 500, 300, 700);

            // Add an empty ListBox field named "CountryList" on page 1
            formEditor.AddField(FieldType.ListBox, "CountryList", 1,
                (float)listBoxRect.LLX, (float)listBoxRect.LLY,
                (float)listBoxRect.URX, (float)listBoxRect.URY);

            // Array of 195 country names
            string[] countries = new string[]
            {
                "Afghanistan","Albania","Algeria","Andorra","Angola","Antigua and Barbuda","Argentina","Armenia","Australia","Austria",
                "Azerbaijan","Bahamas","Bahrain","Bangladesh","Barbados","Belarus","Belgium","Belize","Benin","Bhutan",
                "Bolivia","Bosnia and Herzegovina","Botswana","Brazil","Brunei","Bulgaria","Burkina Faso","Burundi","Cabo Verde","Cambodia",
                "Cameroon","Canada","Central African Republic","Chad","Chile","China","Colombia","Comoros","Congo (Congo‑Brazzaville)","Costa Rica",
                "Croatia","Cuba","Cyprus","Czechia (Czech Republic)","Democratic Republic of the Congo","Denmark","Djibouti","Dominica","Dominican Republic","Ecuador",
                "Egypt","El Salvador","Equatorial Guinea","Eritrea","Estonia","Eswatini (Swaziland)","Ethiopia","Fiji","Finland","France",
                "Gabon","Gambia","Georgia","Germany","Ghana","Greece","Grenada","Guatemala","Guinea","Guinea‑Bissau",
                "Guyana","Haiti","Holy See","Honduras","Hungary","Iceland","India","Indonesia","Iran","Iraq",
                "Ireland","Israel","Italy","Jamaica","Japan","Jordan","Kazakhstan","Kenya","Kiribati","Kuwait",
                "Kyrgyzstan","Laos","Latvia","Lebanon","Lesotho","Liberia","Libya","Liechtenstein","Lithuania","Luxembourg",
                "Madagascar","Malawi","Malaysia","Maldives","Mali","Malta","Marshall Islands","Mauritania","Mauritius","Mexico",
                "Micronesia","Moldova","Monaco","Mongolia","Montenegro","Morocco","Mozambique","Myanmar (Burma)","Namibia","Nauru",
                "Nepal","Netherlands","New Zealand","Nicaragua","Niger","Nigeria","North Korea","North Macedonia","Norway","Oman",
                "Pakistan","Palau","Palestine State","Panama","Papua New Guinea","Paraguay","Peru","Philippines","Poland","Portugal",
                "Qatar","Romania","Russia","Rwanda","Saint Kitts and Nevis","Saint Lucia","Saint Vincent and the Grenadines","Samoa","San Marino","Sao Tome and Principe",
                "Saudi Arabia","Senegal","Serbia","Seychelles","Sierra Leone","Singapore","Slovakia","Slovenia","Solomon Islands","Somalia",
                "South Africa","South Korea","South Sudan","Spain","Sri Lanka","Sudan","Suriname","Sweden","Switzerland","Syria",
                "Tajikistan","Tanzania","Thailand","Timor‑Leste","Togo","Tonga","Trinidad and Tobago","Tunisia","Turkey","Turkmenistan",
                "Tuvalu","Uganda","Ukraine","United Arab Emirates","United Kingdom","United States of America","Uruguay","Uzbekistan","Vanuatu","Venezuela",
                "Vietnam","Yemen","Zambia","Zimbabwe"
            };

            // Add each country as an item in the list box
            foreach (string country in countries)
            {
                formEditor.AddListItem("CountryList", country);
            }

            // Save the changes back to the PDF
            formEditor.Save(pdfPath);
        }
    }
}
