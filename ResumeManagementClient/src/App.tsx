import React, {lazy, Suspense} from "react";
import { useTheme } from "./context/themeContext";
import Navbar from "./components/Navbar/Navbar";
import { Navigate, Route, Routes } from "react-router-dom";
import Spinner from "./utils/Spinner";
import CandidatesPage from "./pages/candidates/CandidatesPage";
import AddCandidatePage from "./pages/candidates/AddCandidatePage";

// Imports with lazy loading
const Home = lazy(()=> import("./pages/home/HomePage"));
const CompaniesPage = lazy(()=> import("./pages/companies/CompaniesPage"));
const AddCompanyPage = lazy(()=> import("./pages/companies/AddCompanyPage"));
const JobsPage = lazy(()=> import("./pages/jobs/JobsPage"));
const AddJobPage = lazy(()=> import("./pages/jobs/AddJobPage"));

function App() {
  const { darkMode } = useTheme();
  const appStyles = darkMode ? "app dark" : "app";
  return (
    <div className={appStyles}>
      <Navbar />
      <div className="wrapper">
        <Suspense fallback={<Spinner/>}>
          <Routes>
            <Route index element={<Navigate replace to="home" />} />
            <Route path="/home" element={<Home/>}/>
            <Route path="/companies">
              <Route index element={<CompaniesPage/>}/>
              <Route path="add" element={<AddCompanyPage/>}/>
            </Route>
            <Route path="/jobs">
              <Route index element={<JobsPage/>}/>
              <Route path="add" element={<AddJobPage/>}/>
            </Route>
            <Route path="/candidates">
              <Route index element={<CandidatesPage/>}/>
              <Route path="add" element={<AddCandidatePage/>}/>
            </Route>
          </Routes>
        </Suspense>
      </div>
    </div>
  );
}

export default App;
