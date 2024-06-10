import axios from 'axios'

const APIServer = axios.create({
    baseURL: "http://localhost:5130/api"
})
export default {
    getAllTasks() {
        return APIServer.get('/tasks')       
    },
    updateTask(updatedTask) {
       return  APIServer.put('/tasks/update', updatedTask)
    },
    addATask(newTask) {
        return APIServer.post('/tasks/create', newTask)
    },
    deleteTask(taskId) {
     return  APIServer.delete(`/tasks/delete/${taskId}`)
     }
}