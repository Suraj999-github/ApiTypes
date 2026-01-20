# ========================================
# SAMPLE GRAPHQL QUERIES (WITH API RESPONSE)
# ========================================

# 1. Get all users with pagination
query GetUsers {
  users(page: 1, pageSize: 5) {
    status
    message
    description
    timestamp
    data {
      items {
        id
        username
        email
        firstName
        lastName
        fullName
        address
        contactNumber
        createdAt
        updatedAt
        createdBy
        updatedBy
      }
      totalCount
      page
      pageSize
      totalPages
    }
  }
}

# 2. Get user by ID
query GetUserById {
  userById(id: 1) {
    status
    message
    description
    id
    timestamp
    data {
      id
      username
      email
      firstName
      lastName
      fullName
      address
      contactNumber
    }
  }
}

# 3. Get user by ID - Not Found Example
query GetUserByIdNotFound {
  userById(id: 999) {
    status
    message
    description
    timestamp
    data {
      id
      username
    }
  }
}

# 4. Get all employees with pagination
query GetEmployees {
  employees(page: 1, pageSize: 10) {
    status
    message
    description
    timestamp
    data {
      items {
        id
        employeeCode
        firstName
        lastName
        fullName
        email
        department
        position
        salary
        hireDate
        contactNumber
        address
        isActive
        managerId
        managerName
        createdAt
        updatedAt
      }
      totalCount
      page
      pageSize
      totalPages
    }
  }
}

# 5. Get employee by ID
query GetEmployeeById {
  employeeById(id: 1) {
    status
    message
    description
    id
    timestamp
    data {
      id
      employeeCode
      firstName
      lastName
      fullName
      email
      department
      position
      salary
      managerName
      isActive
    }
  }
}

# 6. Get employees by department with pagination
query GetEmployeesByDepartment {
  employeesByDepartment(department: "Engineering", page: 1, pageSize: 5) {
    status
    message
    description
    timestamp
    data {
      items {
        id
        employeeCode
        fullName
        position
        salary
        managerName
      }
      totalCount
      totalPages
    }
  }
}

# ========================================
# SAMPLE GRAPHQL MUTATIONS (WITH API RESPONSE)
# ========================================

# 7. Create a new user
mutation CreateUser {
  createUser(
    input: {
      username: "mike.brown"
      email: "mike.brown@company.com"
      firstName: "Mike"
      lastName: "Brown"
      address: "999 Sunset Blvd, Miami, FL"
      contactNumber: "+1-555-0301"
      createdBy: "admin"
    }
  ) {
    status
    message
    description
    id
    timestamp
    data {
      id
      username
      email
      firstName
      lastName
      fullName
      address
      contactNumber
      createdAt
      createdBy
    }
  }
}

# 8. Update a user
mutation UpdateUser {
  updateUser(
    input: {
      id: 1
      firstName: "Jonathan"
      lastName: "Doe"
      address: "456 Updated St, New York, NY"
      updatedBy: "admin"
    }
  ) {
    status
    message
    description
    id
    timestamp
    data {
      id
      username
      firstName
      lastName
      fullName
      address
      updatedAt
      updatedBy
    }
  }
}

# 9. Update user - Not Found Example
mutation UpdateUserNotFound {
  updateUser(
    input: {
      id: 999
      firstName: "Test"
      updatedBy: "admin"
    }
  ) {
    status
    message
    description
    timestamp
  }
}

# 10. Delete a user
mutation DeleteUser {
  deleteUser(id: 3) {
    status
    message
    description
    id
    timestamp
    data
  }
}

# 11. Delete user - Not Found Example
mutation DeleteUserNotFound {
  deleteUser(id: 999) {
    status
    message
    description
    timestamp
    data
  }
}

# 12. Create a new employee
mutation CreateEmployee {
  createEmployee(
    input: {
      employeeCode: "EMP004"
      firstName: "David"
      lastName: "Lee"
      email: "david.lee@company.com"
      department: "Sales"
      position: "Sales Representative"
      salary: 55000
      hireDate: "2024-01-15"
      contactNumber: "+1-555-0401"
      address: "777 Commerce St, Chicago, IL"
      managerId: 2
      createdBy: "admin"
    }
  ) {
    status
    message
    description
    id
    timestamp
    data {
      id
      employeeCode
      fullName
      email
      department
      position
      salary
      managerName
      createdAt
    }
  }
}

# 13. Update an employee
mutation UpdateEmployee {
  updateEmployee(
    input: {
      id: 1
      position: "Lead Developer"
      salary: 110000
      department: "Engineering"
      updatedBy: "admin"
    }
  ) {
    status
    message
    description
    id
    timestamp
    data {
      id
      employeeCode
      fullName
      position
      salary
      department
      updatedAt
      updatedBy
    }
  }
}

# 14. Deactivate an employee (soft delete)
mutation DeactivateEmployee {
  deactivateEmployee(id: 2, updatedBy: "admin") {
    status
    message
    description
    id
    timestamp
    data
  }
}

# 15. Delete an employee (hard delete)
mutation DeleteEmployee {
  deleteEmployee(id: 3) {
    status
    message
    description
    id
    timestamp
    data
  }
}

# ========================================
# ADVANCED QUERIES - ERROR HANDLING
# ========================================

# 16. Query with comprehensive error handling
query GetUserWithErrorHandling {
  userById(id: 1) {
    status
    message
    description
    id
    responseCode
    errorCode
    timestamp
    errors {
      message
      field
      details
    }
    data {
      id
      username
      email
      fullName
    }
  }
}

# 17. Complex nested query
query GetCompleteUserData {
  users(page: 1, pageSize: 3) {
    status
    message
    description
    timestamp
    data {
      items {
        id
        fullName
        email
        contactNumber
        createdAt
      }
      totalCount
      page
      pageSize
      totalPages
    }
  }
  
  employees(page: 1, pageSize: 3) {
    status
    message
    data {
      items {
        id
        fullName
        department
        position
        managerName
      }
      totalCount
    }
  }
}