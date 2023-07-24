import UserTable from "../cardDesign/UserTable.tsx";

function UsersPage() {
    return (
        <div className="flex items-center justify-center h-screen">
            <div className="flex">
                <div className="card-container" justify-center>
                    <UserTable />
                </div>
            </div>
        </div>

    );
}

export default UsersPage;