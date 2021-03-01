# importing required modules  
import PyPDF2  
    
# creating a pdf file object  
pdfFileObj = open('CAE-CD_2020_Knowledge_Units.pdf', 'rb')  
    
# creating a pdf reader object  
pdfReader = PyPDF2.PdfFileReader(pdfFileObj)  
    
# printing number of pages in pdf file  
print(pdfReader.numPages)

# creating a page object  
pageObj = pdfReader.getPage(7)  
    
# extracting text from page  
print(pageObj.extractText())

text = pageObj.extractText()

f = open("page7.txt", "a")
f.write(text)
    
# closing the pdf file object  
pdfFileObj.close()  
    
