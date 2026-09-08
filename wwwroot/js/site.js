document.addEventListener('DOMContentLoaded', function () {
  employeeTable();
});

let currentPage = 1;
const pageSize = 10;
let totalPages = 1;
let totalCount = 0;
let searchEmployeeTerm = '';

const container = document.getElementById('employeeTable');
const searchEmployeeInput = document.getElementById('searchEmployeeInput');

let searchTimeOut;
searchEmployeeInput.addEventListener('input', (e) => {
  clearTimeout(searchTimeOut);
  searchTimeOut = setTimeout(() => {
    searchEmployeeTerm = e.target.value;
    currentPage = 1;
    employeeTable();
  }, 300);
});

const employeeTable = async () => {
  const url = `http://localhost:5001/Home/GetEmployeePage?PageNumber=${currentPage}&PageSize=${pageSize}&SearchEmployee=${searchEmployeeTerm}`;

  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(`Respons Status: ${response.status}`);
    }

    const result = await response.json();
    const employees = result.data;
    const metadata = result.metadata;

    totalPages = metadata.totalPages;
    totalCount = metadata.totalCount;

    if (employees && employees.length > 0) {
      container.innerHTML = renderTable(employees) + renderPagination();
      attachDeleteEvents();
      attachPaginationEvents();
    } else {
      container.innerHTML = '<p class="text-muted">Tidak ada data</p>';
    }
  } catch (err) {
    console.log(err);
    container.innerHTML = '<p class="text-danger">Gagal memuat data</p>';
  }
};

const renderTable = (employees) => {
  const rows = employees
    .map(
      (employee, index) => `
        <tr>
          <th scope="row">${(currentPage - 1) * pageSize + index + 1}.</th>
          <td>${employee.firstName}</td>
          <td>${employee.lastName}</td>
          <td>${employee.departement}</td>
          <td>${employee.hireDate}</td>
          <td>${employee.salary.toLocaleString('id-ID', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
          <td>${employee.email}</td>
          <td>${employee.jobTitle}</td>
          <td>
            <button type="button" class="btn btn-danger btn-delete" data-id="${employee.id}">Delete</button>
          </td>
        </tr>`
    )
    .join('');

  return `
    <table class="table table-light table-striped">
      <thead>
        <tr>
          <th scope="col">No.</th>
          <th scope="col">First Name</th>
          <th scope="col">Last Name</th>
          <th scope="col">Departement</th>
          <th scope="col">Hire Date</th>
          <th scope="col">Salary</th>
          <th scope="col">Email</th>
          <th scope="col">Job Title</th>
          <th scope="col">Action</th>
        </tr>
      </thead>
      <tbody>
        ${rows}
      </tbody>
    </table>`;
};

const renderPagination = () => {
  const startItem = totalCount === 0 ? 0 : (currentPage - 1) * pageSize + 1;
  const endItem = Math.min(currentPage * pageSize, totalCount);

  let pageItems = '';
  for (let i = 1; i <= totalPages; i++) {
    pageItems += `
      <li class="page-item ${i === currentPage ? 'active' : ''}">
        <a class="page-link page-number" href="#" data-page="${i}">${i}</a>
      </li>`;
  }

  return `
    <div class="d-flex justify-content-between align-items-center mt-3">
      <span class="text-muted">
        Display ${startItem} - ${endItem} of ${totalCount} items
      </span>

      <nav aria-label="Page navigation">
        <ul class="pagination mb-0">
          <li class="page-item ${currentPage === 1 ? 'disabled' : ''}">
            <a class="page-link" href="#" id="prevPage">Previous</a>
          </li>
          ${pageItems}
          <li class="page-item ${currentPage === totalPages ? 'disabled' : ''}">
            <a class="page-link" href="#" id="nextPage">Next</a>
          </li>
        </ul>
      </nav>
    </div>`;
};

const attachPaginationEvents = () => {
  const prevBtn = document.getElementById('prevPage');
  const nextBtn = document.getElementById('nextPage');
  const pageNumbers = document.querySelectorAll('.page-number');

  if (prevBtn) {
    prevBtn.addEventListener('click', (e) => {
      e.preventDefault();
      if (currentPage > 1) {
        currentPage -= 1;
        employeeTable();
      }
    });
  }

  if (nextBtn) {
    nextBtn.addEventListener('click', (e) => {
      e.preventDefault();
      if (currentPage < totalPages) {
        currentPage += 1;
        employeeTable();
      }
    });
  }

  pageNumbers.forEach((el) => {
    el.addEventListener('click', (e) => {
      e.preventDefault();
      currentPage = Number(e.target.dataset.page);
      employeeTable();
    });
  });
};

const attachDeleteEvents = () => {
  const deleteButtons = document.querySelectorAll('.btn-delete');

  deleteButtons.forEach((btn) => {
    btn.addEventListener('click', async (e) => {
      const id = e.target.dataset.id;
      const confirmed = confirm('Yakin mau hapus data ini?');
      if (!confirmed) return;

      e.target.disabled = true;
      e.target.textContent = 'Deleting...';

      try {
        const response = await fetch(
          `http://localhost:5001/api/employees/${id}`,
          {
            method: 'DELETE',
          }
        );

        if (!response.ok) {
          const errorData = await response.json().catch(() => null);
          throw new Error(
            errorData?.message || `Gagal menghapus (status ${response.status})`
          );
        }

        const isLastRowOnPage =
          container.querySelectorAll('tbody tr').length === 1;
        if (isLastRowOnPage && currentPage > 1) {
          currentPage -= 1;
        }

        employeeTable();
      } catch (err) {
        alert(err.message);
        e.target.disabled = false;
        e.target.textContent = 'Delete';
      }
    });
  });
};
