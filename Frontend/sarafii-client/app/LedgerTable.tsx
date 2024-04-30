import React from 'react'

interface Customer {
    id: number;
    firstName: string;
    lastName: string;
}

const LedgerTable = async () => {
    const res = await fetch('https://dummyjson.com/users');
    const data = await res.json();
    const users: Customer[] = data.users;
    
  return (
    <>
        <table className='table table-bordered'>
            <thead>
                <tr>
                    <th>First Name</th>
                    <th>Last Name</th>
                </tr>
            </thead>
            <tbody>
                {users.map(user => <tr key={user.id}>
                    <td>{user.firstName}</td>
                    <td>{user.lastName}</td>
                </tr>)}
            </tbody>
        </table>
    </>
  )
}

export default LedgerTable