<template>
  <div>
    <modal modalHeading=" Add new Product" :cond = "showingAddModal" @modalClose = "showingAddModal = false" >
      <table>
        <tr>
          <td>Code</td>
          <td>:</td>
          <td><input type="text" placeholder="Code" v-model="newProduct.code"></td>
        </tr>
        <tr>
          <td>Name</td>
          <td>:</td>
          <td><input type="text" placeholder="Name" v-model="newProduct.name"></td>
        </tr>
        <tr>
          <td>Price</td>
          <td>:</td>
          <td><input type="number" placeholder="Price" v-model.number="newProduct.price"></td>
        </tr>
        <tr>
          <td>Category</td>
          <td>:</td>
          <td><input type="text" placeholder="Category" v-model="newProduct.category"></td>
        <tr>
        <td>Photo_dev</td>
        <td>:</td>
        <td><input type="file" input id="fileItem" @change="onFileSelected"></td>
        <tr>
          <td>
          </td>
          <td>
            <button class="btnSave" @click= "addNewProduct()"> Save</button>
          </td>
        </tr>
      </table>
    </modal>
    <modal modalHeading=" Update" :cond = "showingEditModal" @modalClose = "showingEditModal = false" >
      <table>
        <tr>
          <td>Code</td>
          <td>:</td>
          <td><input type="text" placeholder="Code" v-model="clickedProduct.code"></td>
        </tr>
        <tr>
          <td>Name</td>
          <td>:</td>
          <td><input type="text" placeholder="Name" v-model="clickedProduct.name"></td>
        </tr>
        <tr>
          <td>Price</td>
          <td>:</td>
          <td><input type="number" placeholder="Price" v-model.number="clickedProduct.price"></td>
        </tr>
        <tr>
          <td>Category</td>
          <td>:</td>
          <td><input type="text" placeholder="Category" v-model="clickedProduct.category"></td>
        <tr>
          <td>
          </td>
          <td>
            <button class="btnSave" @click= "updateProduct()"> Save</button>
          </td>
        </tr>
      </table>
    </modal>
    <modal modalHeading=" Delete user" :cond = "showingDeleteModal" @modalClose = "showingDeleteModal = false" >
      <h2>You are going to delete the product '{{clickedProduct.name}}'</h2>
      <table>
        <tr>
        </tr>
        <tr>
          <td>
          </td>
          <td>
            <button class="btnSave" @click= "DeleteProduct()"> Yes</button>
          </td>
          <td>
            <button class="btnSave" @click= "showingDeleteModal = false"> No</button>
          </td>
        </tr>
      </table>
    </modal>
    <h2 class="fleft">Product</h2>
    <button class="addBtn fright" @click="showingAddModal = true">Add New</button>
    <br><br><br>
    <hr>
    <h2 class="fleft">Filters</h2>
    <br><br>
    <table>
      <th>Category</th>
      <td>
        <select v-model="filterCategory" @change="filtersByProduct(filterName,filterCategory)" >
          <option>All</option>
          <option v-for="product in Products"> {{product.category}}</option>
        </select>
      </td>

    </table>
    <button class="addBtn fleft" @click="clearFilters">Clear Filters</button>

    <div class="clear"></div>
    <hr>
    <table class="nice-table">
      <tr>
        <th>Code</th>
        <th>Name</th>
        <th>Price</th>
        <th>Category</th>
        <th>Edit</th>
        <th>delete</th>
      </tr>
      <tr v-for="product in filtersProduct">
        <td>{{product.code}}</td>
        <td>{{product.name}}</td>
        <td>{{product.price}}</td>
        <td>{{product.category}}</td>
        <td><button class="edit" @click=" showingEditModal = true , clickedProduct = product " >Edit</button></td>
        <td><button class="delete" @click="showingDeleteModal = true, clickedProduct = product">Delete</button></td>
      </tr>
    </table>
  </div>
</template>
<script>
    export default {
        name: 'Users',
        data () {
            return {
                file1: '',
                showingAddModal:false,
                showingEditModal:false,
                showingDeleteModal:false,
                filterName:"",
                filterCategory:"",
                 newProduct:{
                    code:"",
                    name:"",
                    price:0,
                    category:"",
                     file:null

                 },
                Products:[],
                clickedProduct:{},
                sortedProduct:[],
                selectedFile:[0],


            }
        },
        mounted(){
          //  console.log("Mounted")
            this.init();
        },
        methods:{
            onFileSelected(event){
            //this.selectedFile = event.target.files[0]
                //console.log(event)

                this.file1 = this.$refs.file.files[0];
                let formData = new FormData();
                formData.append('file', this.file);
                console.log(this.formData)

            },
            init(){
                this.$eventBus.$emit("loadingStatus", true);
                this.$axios.get("http://localhost:56750/api/Product/getall", ).then(res =>{
                    this.$eventBus.$emit("loadingStatus", false)
                    if(res.data.error){
                        this.$iziToast.error({
                            title: 'Error',
                            message: res.data.message,
                        });
                    }
                    else {
                        this.Products = res.data.data;
                      //  console.log(this.Products)
                    }
                })

            },
            clearFilters(){
                this.filterName = null,
                    this.filterCategory= null
                this.sortedProduct = []

            },

            addNewProduct(){
                let userToken= 'Bearer' +" " +localStorage.getItem("token");
                this.$eventBus.$emit("loadingStatus", true);
                this.$axios.post("http://localhost:56750/api/Product", this.newProduct, {headers:{ 'Authorization': userToken}}).then(res =>{
                    this.$eventBus.$emit("loadingStatus", false)
                    if(res.data.success){
                        this.$axios.defaults.headers.common['Authorization'] = 'token' +localStorage.getItem("token");
                       // console.log();
                        this.showingAddModal = false
                        this.init();
                    }
                    else {
                        this.$iziToast.error({
                            title: 'Error',
                            message: "\n" +
                                "The product already exists.",
                        });
                    }
                })

            },

            updateProduct(){
                let userToken= 'Bearer' +" " +localStorage.getItem("token");
                this.$eventBus.$emit("loadingStatus", true);
                this.$axios.put("http://localhost:56750/Api/product/", this.clickedProduct, {headers:{ 'Authorization': userToken}}).then(res =>{
                    this.$eventBus.$emit("loadingStatus", false)
                    if(res.data.success){
                        this.$axios.defaults.headers.common['Authorization'] = 'token' +localStorage.getItem("token");
                        //this.$router.push({name:"admin"})
                        //var xx = localStorage.getItem("token")
                        this.showingEditModal = false
                        this.init();
                    }
                    else {
                        this.$iziToast.error({
                            title: 'Error',
                            message: "\n" +
                                "",
                        });
                    }
                })

            },

            DeleteProduct(){
                let userToken= 'Bearer' +" " +localStorage.getItem("token");
                let deleteUrl = 'http://localhost:56750/Api/product/' + this.clickedProduct.id
                this.$eventBus.$emit("loadingStatus", true);
                this.$axios.delete(deleteUrl, {headers:{ 'Authorization': userToken}} ).then(res =>{
                  //  console.log(res.data)
                    this.$eventBus.$emit("loadingStatus", false)
                  //  console.log();
                    if(res.data.success){
                        this.$axios.defaults.headers.common['Authorization'] = 'token' +localStorage.getItem("token");
                        //this.$router.push({name:"admin"})
                        //var xx = localStorage.getItem("token")
                        this.showingDeleteModal = false
                        this.init();


                    }
                    else {

                        this.$iziToast.error({
                            title: 'Error',
                            message: "\n" +
                                "",
                        });
                    }
                })
            },
            filtersByProduct(name, category){
                let vm = this;
                this.sortedProduct = []
                //console.log(this.filterLogin)
                this.Products.map(function (item) {
                    if (item.name === name || item.category === category)
                        vm.sortedProduct.push(item)
                })
            },
        },
        computed:{
            filtersProduct(){
                if (this.sortedProduct.length){
                    return this.sortedProduct
                }
                else {
                    return this.Products
                }
            },
        }
    }
</script>



