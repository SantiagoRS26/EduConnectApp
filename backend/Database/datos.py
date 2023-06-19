import requests
import pyodbc


conn = pyodbc.connect('DRIVER={SQL Server};SERVER=REVISION-PC;DATABASE=EduConnectPruebas;Trusted_Connection=yes;TrustServerCertificate=yes;')


api_url = 'https://www.datos.gov.co/resource/xdk5-pm3f.json'


headers = {
    'X-App-Token': 'eFMFD7ipnG8vglL3Bc5lY7Tyg'
}


response = requests.get(api_url, headers=headers)


if response.status_code == 200:
    data = response.json()


    for item in data:
        department_code = item['c_digo_dane_del_departamento']
        department_name = item['departamento']
        city_code = item['c_digo_dane_del_municipio']
        city_name = item['municipio']


        department_check_query = "SELECT COUNT(*) FROM Departments WHERE DepartmentID = ?"
        existing_department_count = conn.execute(department_check_query, department_code).fetchone()[0]

        if existing_department_count == 0:

            department_query = "INSERT INTO Departments (DepartmentID, DepartmentName) VALUES (?, ?)"
            conn.execute(department_query, department_code, department_name)


        city_query = "INSERT INTO Cities (CityID, CityName, DepartmentID) VALUES (?, ?, ?)"
        conn.execute(city_query, city_code, city_name, department_code)


    conn.commit()


conn.close()
