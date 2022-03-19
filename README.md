# TMProject2Json
Extracts data from a Teammate+ Audit project into a json format.

We are using Teammate+ Audit software to document the internal audit work. The challenge is that the build in report generator is flawed. Therefore I have created this extractor. It connects to the production database of the application and extracts a list of projects. After choosing one, the content will be converted to json and saved in a predefined location as file (Google Drive). Then a specific URL is called in Chrome, a script there will transfer the json file into a Google document, which is the report for the audit.
