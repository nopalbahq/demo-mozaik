document.addEventListener('DOMContentLoaded', function () {
  employeeTable();
});

const employeeTable = async () => {
  const url = 'http://localhost:5001/Home/GetEmployeePage';
  const container = document.getElementById('employeeTable');
  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(`Respons Status: ${response.status}`);
    }
    const data = await response.json();

    if (data && data.length > 0) {
      container.innerHTML = renderTable(data);
    } else {
      container.innerHTML = '<p class="text-muted"> Tidak ada data </p>';
    }
    console.log(data);
  } catch (err) {
    console.log(err);
  }
};

const renderTable = (employees) => {
  const rows = employees
    .map(
      (employee, index) =>
        `
  <tr>
      <th scope="row">${index + 1}</th>
      <td>${employee.firstName}</td>
      <td>${employee.lastName}</td>
      <td>${employee.departement}</td>
      <td>${employee.hireDate}</td>
      <td>${employee.salary.toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
      <td>${employee.email}</td>
      <td>${employee.jobTitle}</td>
      <td>
        <button type="button" class="btn btn-danger">Delete</button>
      </td>
    </tr>`
    )
    .join('');

  return `
    <table class="table table-light table-striped">
      <thead>
        <tr>
          <th scope="col">No.</th>
          <th scope="col">FirstName</th>
          <th scope="col">LastName</th>
          <th scope="col">Departement</th>
          <th scope="col">Hire Date</th>
          <th scope="col">Salary</th>
          <th scope="col">Email</th>
          <th scope="col">Job Tittle</th>
          <th scope="col">Action</th>
        </tr>
      </thead>
      <tbody>
        ${rows}
      </tbody>
    </table>`;
};

console.log('TEST SITE JS');
