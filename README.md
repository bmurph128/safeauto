Run solution and then select "Upload File" - uploaded files must be in one of two formats:

Driver Name

Trip Name StartTime EndTime MilesDriven

Once file is uploaded, the file goes from the frontend (Angular) to the backend (C#), using an API controller (UploadController). The controller sends the file to CustomerFileUploadService class which reads it in, splits the file and creates a Driver or Trip object. Each Driver is added to a list, and each Trip for a specific Driver is added to their own list of trips. Each list is looped through to isolate individual trips to verify that the trip qualifies to be returned (between 5 and 100 MPH). The time and miles driven are then added for each Trip for each Driver and creates a DriverDto object (names, total miles driver, average MPH) which is added to a list. The list is then sorted by most total miles driven and returned to the frontend, where Angular is used to display the DriverDto objects in a table.